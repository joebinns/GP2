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
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.Update);
            _input.UnsubscribeFloat(OnReachInput, InputType.Reach);
        }
        
// INPUT

        /// <summary>
        /// Updates hold distance direction on input callback
        /// </summary>
        private void OnReachInput(float direction){
            if (direction == 0f) return;
            _direction = Mathf.Sign(direction);
        }
        
// MOVEMENT

        /// <summary>
        /// Checks hold distance direction on controller update
        /// </summary>
        public override bool OnCheck() {
            if (!_playerHold.IsHolding) SetHoldDistance(_settings.DefaultHoldDistance); // Reset the hold distance between holding
            return _direction != 0 && _playerHold.IsHolding;
        }

        /// <summary>
        /// Moves hold pivot on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime) {
            var holdDistanceRange = _settings.HoldDistanceRange;
            var reach = Mathf.Clamp( _holdPivot.localPosition.z + _direction * _settings.HoldDistanceSensitivity * deltaTime, holdDistanceRange.x, holdDistanceRange.y);
            SetHoldDistance(reach);
        }

        private void SetHoldDistance(float distance) {
            var holdPosition = _holdPivot.localPosition;
            holdPosition.z = distance;
            _holdPivot.localPosition = holdPosition;
        }
    }
}
