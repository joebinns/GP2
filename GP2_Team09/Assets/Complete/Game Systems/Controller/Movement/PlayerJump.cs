using System;
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
            if (!_playerFall.IsGrounded) return;
            StartCoroutine(Jump());
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void OnExit() {
            base.OnExit();
            if (State != JumpState.Rising) return;
            StopCoroutine(_jumpRise);
            State = JumpState.Default;
        }

        private IEnumerator Jump() {
            State = JumpState.Rising;
            yield return _jumpRise = StartCoroutine(JumpRise());
            State = JumpState.Default;
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

        /// <summary>
        /// Move character controller vertically, given a desired and previous vertical position
        /// </summary>
        private void SetVerticalPosition(float deltaVerticalPosition) => _characterController.Move(Vector3.up * (0.5f * deltaVerticalPosition));
    }
}
