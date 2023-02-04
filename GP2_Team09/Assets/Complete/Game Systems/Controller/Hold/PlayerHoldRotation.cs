using SF = UnityEngine.SerializeField;
using GameProject.Actions;
using GameProject.Inputs;
using GameProject.Interactions;
using GameProject.Movement;
using UnityEngine;

namespace GameProject.Hold
{
    [RequireComponent(typeof(PlayerHold), typeof(PlayerLook), typeof(PlayerTurn))]
    public class PlayerHoldRotation : BaseAction
    {
        [SF] private PlayerController _controller = null;
        [Space]
        [SF] private MovementSettings _settings = null;
        [SF] private InputManager _input = null;
        
        private PlayerHold _playerHold;
        private PlayerLook _playerLook;
        private PlayerTurn _playerTurn;
        private Transform _holdPivot;
        private Vector2 _angle;
        private Vector2 _direction;
        private bool _isRotatable;
        private bool _isHolding;
        
// INITIALISATION

        /// <summary>
        /// Initialise frequently used references
        /// </summary>
        private void Awake() {
            _playerHold = GetComponent<PlayerHold>();
            _playerLook = GetComponent<PlayerLook>();
            _playerTurn = GetComponent<PlayerTurn>();
            _holdPivot = _playerHold.HoldPivot;
        }

        /// <summary>
        /// Adds this action to the player controller
        /// </summary>
        private void OnEnable(){
            _controller.AddAction(this, UpdateMode.LateUpdate);
            _input.SubscribeKey(OnSecondaryInput, InputType.Secondary, Priority);
            _input.SubscribeFloat(OnLookInput, InputType.Tilt, Priority);
            _input.SubscribeFloat(OnTurnInput, InputType.Turn, Priority);
            _playerHold.OnGrab += OnGrab;
            _playerHold.OnRelease += OnRelease;
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.LateUpdate);
            _input.UnsubscribeKey(OnSecondaryInput, InputType.Secondary);
            _input.UnsubscribeFloat(OnLookInput, InputType.Tilt);
            _input.UnsubscribeFloat(OnTurnInput, InputType.Turn);
            _playerHold.OnGrab -= OnGrab;
            _playerHold.OnRelease -= OnRelease;
        }

// INPUT

        /// <summary>
        /// Updates look direction on input callback
        /// </summary>
        private void OnLookInput(float direction){
            _direction.y = -direction;
        }
        
        /// <summary>
        /// Updates turn direction on input callback
        /// </summary>
        private void OnTurnInput(float direction){
            _direction.x = direction;
        }
        
        /// <summary>
        /// Updates secondary on input callback
        /// </summary>
        private void OnSecondaryInput(){
            if (_isHolding) _isRotatable = !_isRotatable;
            else _isRotatable = false;
            
            //_input.SetInputState(InputType.Tilt, !_isRotatable); this disabled tilt input for both look and rotation
            _playerLook.enabled = !_isRotatable;
            _playerTurn.enabled = !_isRotatable;
        }
        
        private void OnGrab(HoldInteraction toHold) {
            // Set variables
            _isHolding = true;
        }
        
        private void OnRelease() {
            // Set variables
            _isHolding = false;
        }
        
// MOVEMENT

        /// <summary>
        /// Checks look input on controller update
        /// </summary>
        public override bool OnCheck() {
            return _isRotatable && _direction != Vector2.zero;
        }

        /// <summary>
        /// Rotate hold pivot on controller update
        /// </summary>
        public override void OnUpdate(float deltaTime) {
            var deltaAngle = new Vector3(_direction.y * _settings.LookSensi * 0.5f, _direction.x * _settings.TurnSensi * 0.5f, 0f);
            _holdPivot.Rotate(_holdPivot.parent.TransformDirection(deltaAngle), Space.World);
        }
    }
}
