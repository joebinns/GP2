using SF = UnityEngine.SerializeField;
using System.Collections;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;
using Unity.VisualScripting;

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

// INITIALISATION

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
        /// Initiate crouching transition
        /// </summary>
        public override void OnEnter() {
            base.OnEnter();
            StartCoroutine(Jump());
        }

        private IEnumerator Jump() {
            yield return StartCoroutine(JumpRise());
            yield return StartCoroutine(Fall());
        }

        /// <summary>
        /// Initiate standing transition
        /// </summary>
        public override void OnExit() {
            base.OnExit();
            //StartCoroutine(Fall());
        }
        
        private IEnumerator JumpRise() {
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
        }

        /// <summary>
        /// Move character controller vertically, given a desired and previous vertical position
        /// </summary>
        private void SetVerticalPosition(float deltaVerticalPosition) => _characterController.Move(Vector3.up * (0.5f * deltaVerticalPosition));
    }
}
