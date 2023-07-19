using System.Collections;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;

namespace GameProject.Movement
{
    [RequireComponent(typeof(PlayerFall))]
    public class PlayerJump : BaseAction
    {
        [SerializeField] private PlayerController _controller = null;
        [SerializeField] private CharacterController _characterController = null;
        [Space]
        [SerializeField] private MovementSettings _settings = null;
        [SerializeField] private InputManager _input = null;
        
        private bool _isJumping;
        private bool _isRising;
        private PlayerFall _playerFall;
        private float _time;
        private bool _shouldCancel;
        
        public JumpState State { get; private set; } = JumpState.Default;

        public enum JumpState {
            Rising,
            Default
        }

// INITIALISATION

        /// <summary>
        /// Store frequently used references
        /// </summary>
        private void Awake() {
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
        /// Trigger a jump
        /// </summary>
        public override void OnEnter() {
            base.OnEnter();
            StartCoroutine(Jump());
        }
        
        /// <summary>
        /// Trigger a jump cancel
        /// </summary>
        public override void OnExit() {
            base.OnExit();
            if (State != JumpState.Rising) return;
            _shouldCancel = true;
        }

        /// <summary>
        /// Start jumping once input buffer and coyote time are satisfied (if not satisfied, then exit)
        /// </summary>
        private IEnumerator Jump() {
            var t = 0f;
            var jumpInputBuffer = _settings.JumpInputBuffer;
            while (t < jumpInputBuffer) {
                t += Time.deltaTime;
                if (_playerFall.TimeSinceUngrounded < _settings.CoyoteTime) {
                    _playerFall.CancelFall();
                    State = JumpState.Rising;
                    yield return StartCoroutine(JumpRise());
                    State = JumpState.Default;
                    t = jumpInputBuffer;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        
        /// <summary>
        /// Rise following the rise curve's initial gradient, fall off following the rise curve
        /// </summary>
        private IEnumerator JumpRise() {
            var time = 0f;
            var curve = _settings.JumpRiseCurve;
            var finalFrame = curve[curve.length - 1];
            var cancelThreshold = _settings.RiseCancelThreshold * finalFrame.time;
            var maxThreshold = _settings.RiseMaximumThreshold * finalFrame.time;

            // Begin rising along the rise curve's initial gradient
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

            // Follow the rise curve
            time = 0f;
            var previousValue = curve.Evaluate(time);
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
