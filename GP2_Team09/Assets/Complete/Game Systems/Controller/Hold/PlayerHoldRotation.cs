using SF = UnityEngine.SerializeField;
using GameProject.Actions;
using GameProject.Inputs;
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
        
// INITIALISATION

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
        }

        /// <summary>
        /// Removes this action from the player controller
        /// </summary>
        private void OnDisable(){
            _controller.RemoveAction(this, UpdateMode.LateUpdate);
            _input.UnsubscribeKey(OnSecondaryInput, InputType.Secondary);
            _input.UnsubscribeFloat(OnLookInput, InputType.Tilt);
            _input.UnsubscribeFloat(OnTurnInput, InputType.Turn);
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
        
        private void OnSecondaryInput(){
            if (_playerHold.IsHolding) _isRotatable = !_isRotatable;
            else _isRotatable = false;
            
            //_input.SetInputState(InputType.Tilt, !_isRotatable); this disabled tilt input for both look and rotation
            GetComponent<PlayerLook>().enabled = !_isRotatable;
            GetComponent<PlayerTurn>().enabled = !_isRotatable;
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
            /*
            _angle.x += _direction.y * _settings.LookSensi;
            _angle.y += _direction.x * _settings.TurnSensi;
            
            _holdPivot.rotation = Quaternion.Inverse(_holdPivot.localRotation)
                                  * Quaternion.Euler(_angle.x, _angle.y, 0);;
            */
            
            /*
            // Get input in CAMERA SPACE
            var deltaRotation = Quaternion.Euler(new Vector3(_direction.y * _settings.LookSensi, _direction.x * _settings.TurnSensi, 0f));
            
            // Convert input from CAMERA SPACE to WORLD SPACE
            deltaRotation = _holdPivot.parent.rotation * deltaRotation; // ?
            
            // Convert from WORLD SPACE to PIVOT SPACE
            deltaRotation = Quaternion.Inverse(_holdPivot.rotation) * deltaRotation;
            
            // Apply
            _holdPivot.localRotation *= deltaRotation;
            */

            var deltaAngle = new Vector3(_direction.y * _settings.LookSensi, _direction.x * _settings.TurnSensi, 0f);
            deltaAngle = _holdPivot.parent.TransformDirection(deltaAngle);
            _holdPivot.Rotate(deltaAngle, Space.World);

        }
    }
}
