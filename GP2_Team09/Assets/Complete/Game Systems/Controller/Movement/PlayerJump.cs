using SF = UnityEngine.SerializeField;
using System.Collections;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;

namespace GameProject.Movement
{
    [RequireComponent(typeof(PlayerFall))]
    public class PlayerJump : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private CharacterController _characterController = null;
        [Space]
        [SF] private MovementSettings _settings = null;
        [SF] private InputManager _input = null;
        
        private bool _isJumping;
        private Coroutine _jumpRise;
        private bool _isRising;
        private PlayerFall _playerFall;
        public JumpState State = JumpState.Default;
        private float _time;
        private bool _shouldCancel;

        public enum JumpState {
            Rising,
            Default
        }

// INITIALISATION

        private void Awake()
        {
            _playerFall = GetComponent<PlayerFall>();
        }

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable() {
            _controller.AddAction(this, UpdateMode.Update);
            _input.SubscribeFloat(OnJumpInput, InputType.Jump, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable() {
            _controller.RemoveAction(this, UpdateMode.Update);
            _input.UnsubscribeFloat(OnJumpInput, InputType.Jump);
        }

// INPUT

        /// <summary>
        /// Toggle jumping
        /// </summary>
        private void OnJumpInput(float duration) {
            _isJumping = !_isJumping;
        }

// MOVEMENT

        /// <summary>
        /// Checks is crouched on controller update
        /// </summary>
        public override bool OnCheck() {
            return _isJumping;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnEnter() {
            base.OnEnter();
            StartCoroutine(Jump());
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void OnExit() {
            base.OnExit();
            if (State != JumpState.Rising) return;
            _shouldCancel = true;
        }

        private IEnumerator Jump() {
            var t = 0f;
            var jumpInputBuffer = _settings.JumpInputBuffer;
            while (t < jumpInputBuffer) {
                t += Time.deltaTime;
                if (_playerFall.TimeSinceUngrounded < _settings.CoyoteTime) {
                    _playerFall.CancelFall();
                    State = JumpState.Rising;
                    yield return _jumpRise = StartCoroutine(JumpRise());
                    State = JumpState.Default;
                    t = jumpInputBuffer;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        
        private IEnumerator JumpRise() {
            var time = 0f;
            var curve = _settings.JumpRiseCurve;
            var cancelThreshold = _settings.RiseCancelThreshold * curve[curve.length - 1].time;
            var maxThreshold = cancelThreshold * 2f;

            // Begin rising along the same trajectory
            var shouldCancel = false;
            var velocityY = curve.Differentiate(curve.Evaluate(time));
            while (!shouldCancel) {
                time += Time.deltaTime;
                if (time >= maxThreshold) _shouldCancel = true;
                SetVerticalPosition(velocityY * Time.deltaTime);
                shouldCancel = _shouldCancel && time >= cancelThreshold;
                yield return new WaitForEndOfFrame();
            }
            _shouldCancel = false;

            time = 0f;
            var previousValue = curve.Evaluate(time);
            var finalFrame = curve[curve.length - 1];
            while (time < finalFrame.time) {
                time += Time.deltaTime;
                var value = curve.Evaluate(time);
                SetVerticalPosition(value - previousValue);
                previousValue = value;
                yield return new WaitForEndOfFrame();
            }
            SetVerticalPosition(finalFrame.value - previousValue);
        }

        /// <summary>
        /// Move character controller vertically, given a desired and previous vertical position
        /// </summary>
        private void SetVerticalPosition(float deltaVerticalPosition) => _characterController.Move(Vector3.up * (0.5f * deltaVerticalPosition));
    }
}
