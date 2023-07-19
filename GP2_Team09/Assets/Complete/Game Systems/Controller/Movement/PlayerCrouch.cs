using System.Collections;
using UnityEngine;
using GameProject.Actions;
using GameProject.Inputs;

namespace GameProject.Movement
{
    public class PlayerCrouch : BaseAction
    {
        [SerializeField] private PlayerController _controller = null;
        [SerializeField] private CharacterController _characterController = null;
        [Space]
        [SerializeField] private MovementSettings _settings = null;
        [SerializeField] private InputManager _input = null;

        private bool _isCrouched;
        private float _height;
        private Vector3 _center;
        
        public CrouchState State { get; private set; } = CrouchState.Default;

        public enum CrouchState {
            Crouching,
            Crouched,
            Standing,
            Default
        }
        
// INITIALISATION

        /// <summary>
        /// Store frequently used variables
        /// </summary>
        private void Awake() {
            _height = _characterController.height;
            _center = _characterController.center;
        }

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable() {
            _controller.AddAction(this, UpdateMode.Update);
            _input.SubscribeKey(OnCrouchInput, InputType.Crouch, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable() {
            _controller.RemoveAction(this, UpdateMode.Update);
            _input.UnsubscribeKey(OnCrouchInput, InputType.Crouch);
        }

// INPUT

        /// <summary>
        /// Toggle crouching
        /// </summary>
        private void OnCrouchInput() { _isCrouched = !_isCrouched; }

// MOVEMENT

        /// <summary>
        /// Checks is crouched on controller update
        /// </summary>
        public override bool OnCheck() => _isCrouched;

        /// <summary>
        /// Initiate crouching transition
        /// </summary>
        public override void OnEnter() {
            base.OnEnter();
            StartCoroutine(Crouch());
        }

        /// <summary>
        /// Initiate standing transition
        /// </summary>
        public override void OnExit() {
            base.OnExit();
            StartCoroutine(Stand());
        }

        /// <summary>
        /// Crouch
        /// </summary>
        private IEnumerator Crouch() {
            State = CrouchState.Crouching;
            yield return StartCoroutine(Transition(_settings.CrouchCurve));
            State = CrouchState.Crouched;
        }
        
        /// <summary>
        /// Stand
        /// </summary>
        private IEnumerator Stand() {
            State = CrouchState.Standing;
            yield return StartCoroutine(Transition(_settings.StandCurve));
            State = CrouchState.Default;
        }

        /// <summary>
        /// Transition to and from crouching by manipulating height and vertical position
        /// </summary>
        private IEnumerator Transition(AnimationCurve curve) {
            var t = 0f;
            var previousValue = curve.Evaluate(t);
            var finalFrame = curve[curve.length - 1];
            while (t < finalFrame.time) {
                t += Time.deltaTime;
                var value = curve.Evaluate(t);
                SetHeight(value);
                SetVerticalPosition(value - previousValue);
                SetCenter(value);
                previousValue = value;
                yield return new WaitForEndOfFrame();
            }
            SetHeight(finalFrame.value);
            SetVerticalPosition(finalFrame.value - previousValue);
            SetCenter(finalFrame.value);
        }
        
        /// <summary>
        /// Set height of the character controller given a relative height
        /// </summary>
        private void SetHeight(float relativeHeight) => _characterController.height = relativeHeight * _height;
        
        /// <summary>
        /// Move character controller vertically, given a desired and previous vertical position
        /// </summary>
        private void SetVerticalPosition(float deltaVerticalPosition) => _characterController.Move(Vector3.up * (_height * deltaVerticalPosition));
        
        /// <summary>
        /// Set center of the character controller given a relative center
        /// </summary>
        private void SetCenter(float relativeCenter) => _characterController.center = _center + Vector3.up * (0.5f * _height * (1 - relativeCenter));
    }
}
