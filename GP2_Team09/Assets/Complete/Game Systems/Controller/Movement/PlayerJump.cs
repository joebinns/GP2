using SF = UnityEngine.SerializeField;
using System.Collections;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;

namespace GameProject.Movement
{
    public class PlayerJump : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [SF] private CharacterController _characterController = null;
        [Space]
        [SF] private MovementSettings _settings = null;
        [SF] private InputManager _input = null;

        private bool _isJumping;
        private JumpState _jumpState;
        private Coroutine _jumpRise;
        private Vector3 _gravity;
        
        private enum JumpState {
            Rising,
            Falling
        }

// INITIALISATION

        /// <summary>
        /// Fetch values from frequently used references
        /// </summary>
        private void Awake() {
            _gravity = Physics.gravity;
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
            if (_jumpState != JumpState.Rising) return;
            StopCoroutine(_jumpRise);
            StartCoroutine(Fall());
        }

        private IEnumerator Jump() {
            yield return _jumpRise = StartCoroutine(JumpRise());
            yield return StartCoroutine(Fall());
        }
        
        /// <summary>
        /// Check if the character controller is grounded
        /// </summary>
        private bool IsGrounded => Physics.SphereCast(transform.position + _characterController.center,
            _characterController.radius, _gravity.normalized, out var hit, 
            (_characterController.height * 0.51f - _characterController.radius * 0.5f));

        private IEnumerator JumpRise() {
            _jumpState = JumpState.Rising;
            var t = 0f;
            var curve = _settings.JumpRiseCurve;
            var previousValue = curve.Evaluate(t);
            var finalFrame = curve[curve.length - 1];
            while (t < finalFrame.time) {
                t += Time.deltaTime;
                var value = curve.Evaluate(t);
                SetVerticalPosition(value - previousValue);
                previousValue = value;
                yield return new WaitForEndOfFrame();
            }
            SetVerticalPosition(finalFrame.value - previousValue);
        }
        
        private IEnumerator Fall() {
            _jumpState = JumpState.Falling;
            var t = 0f;
            var curve = _settings.FallCurve;
            var previousValue = curve.Evaluate(t);
            var finalFrame = curve[curve.length - 1];
            while (t < finalFrame.time) {
                t += Time.deltaTime;
                var value = curve.Evaluate(t);
                SetVerticalPosition(value - previousValue);
                previousValue = value;
                yield return new WaitForEndOfFrame();
            }
            SetVerticalPosition(finalFrame.value - previousValue);
            
            
            // Continue falling along the same trajectory, until _grounded
            var velocityY = curve.Differentiate(curve[curve.length - 1].time);
            while (!IsGrounded) {
                SetVerticalPosition(velocityY * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Move character controller vertically, given a desired and previous vertical position
        /// </summary>
        private void SetVerticalPosition(float deltaVerticalPosition) => _characterController.Move(Vector3.up * (0.5f * deltaVerticalPosition));
    }
}
