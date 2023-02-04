using SF = UnityEngine.SerializeField;
using GameProject.Actions;
using GameProject.Inputs;
using GameProject.Movement;
using UnityEngine;

namespace GameProject.Hold
{
    [RequireComponent(typeof(PlayerHold))]
    public class PlayerHoldRotation : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [Space]
        [SF] private MovementSettings _settings = null;
        [SF] private InputManager _input = null;

        private PlayerHold _playerHold;
        private Transform _holdPivot;
        private float _angle = 0;
        private float _direction = 0;
        private bool _isRotatable;
        
// INITIALISATION

        private void Awake() {
            _playerHold = GetComponent<PlayerHold>();
            _holdPivot = _playerHold.HoldPivot;
        }

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.LateUpdate);
            _input.SubscribeKey(OnSecondaryInput, InputType.Secondary, Priority);
            _input.SubscribeFloat(OnLookInput, InputType.Tilt, Priority);
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.LateUpdate);
            _input.UnsubscribeFloat(OnLookInput, InputType.Tilt);
        }
        
// INPUT

        /// <summary>
        /// Updates look direction on input callback
        /// </summary>
        private void OnLookInput(float direction){
            _direction = -direction;
        }
        
        private void OnSecondaryInput() {
            _isRotatable = _playerHold.IsHolding && !_isRotatable;
        }
        
// MOVEMENT

        /// <summary>
        /// Checks look input on controller update
        /// </summary>
        public override bool OnCheck() {
            return _isRotatable && _direction != 0;
        }

        /// <summary>
        /// Rotate hold pivot on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime) {
            _angle += _direction * _settings.LookSensi;
            _holdPivot.localRotation = Quaternion.Euler(_angle, 0, 0);
        }
    }
}
