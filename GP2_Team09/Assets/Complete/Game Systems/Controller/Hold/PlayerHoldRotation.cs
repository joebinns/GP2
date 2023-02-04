using SF = UnityEngine.SerializeField;
using GameProject.Actions;
using GameProject.Inputs;
using GameProject.Interactions;
using GameProject.Movement;
using UnityEngine;

namespace GameProject.Hold
{
    [RequireComponent(typeof(PlayerHold))]
    public class PlayerHoldRotation : BaseAction
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
            _direction = direction;
        }
        
// MOVEMENT

        /// <summary>
        /// Checks hold distance direction on controller update
        /// </summary>
        public override bool OnCheck() {
            return _direction != 0 && _playerHold.IsHolding;
        }

        /// <summary>
        /// Moves hold pivot on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime) {
        }
    }
}
