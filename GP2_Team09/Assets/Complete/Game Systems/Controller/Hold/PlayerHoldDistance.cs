using SF = UnityEngine.SerializeField;
using GameProject.Actions;
using GameProject.Inputs;
using GameProject.Interactions;
using GameProject.Movement;
using UnityEngine;

namespace GameProject.Hold
{
    [RequireComponent(typeof(PlayerHold))]
    public class PlayerHoldDistance : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [Space]
        [SF] private InteractionSettings _settings = null;
        [SF] private InputManager _input = null;

        private PlayerHold _playerHold;
        private Transform _holdPivot;
        private float _direction = 0;
        private bool _isHolding;
        
// INITIALISATION

        private void Awake() {
            _playerHold = GetComponent<PlayerHold>();
            _holdPivot = _playerHold.HoldPivot;
        }

        private void Start() {
            SetHoldDistance(_settings.DefaultHoldDistance);
        }

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.Update);
            _input.SubscribeFloat(OnReachInput, InputType.Reach, Priority);
            _playerHold.OnGrab += OnGrab;
            _playerHold.OnRelease += OnRelease;
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.Update);
            _input.UnsubscribeFloat(OnReachInput, InputType.Reach);
            _playerHold.OnGrab -= OnGrab;
            _playerHold.OnRelease -= OnRelease;
        }
        
// INPUT

        /// <summary>
        /// Updates hold distance direction on input callback
        /// </summary>
        private void OnReachInput(float direction) {
            _direction = direction switch {
                > 0f => 1f,
                < 0f => -1f,
                _ => 0f
            };
        }

        private void OnGrab(HoldInteraction toHold) {
            // Set variables
            _isHolding = true;
            
            // Set hold distance
            SetHoldDistance(Vector3.Distance(_holdPivot.parent.position, toHold.transform.position));
        }
        
        private void OnRelease() {
            // Set variables
            _isHolding = false;
        }
        
// MOVEMENT

        /// <summary>
        /// Checks hold distance direction on controller update
        /// </summary>
        public override bool OnCheck() {
            return _direction != 0 && _isHolding;
        }

        /// <summary>
        /// Moves hold pivot on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime) {
            SetHoldDistance(_holdPivot.localPosition.z + _direction * _settings.HoldDistanceSensitivity * deltaTime);
        }

        private void SetHoldDistance(float distance) {
            var holdDistanceRange = _settings.HoldDistanceRange;
            var holdPosition = _holdPivot.localPosition;
            holdPosition.z = Mathf.Clamp(distance, holdDistanceRange.x, holdDistanceRange.y);
            _holdPivot.localPosition = holdPosition;
        }
    }
}
