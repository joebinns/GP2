using SF = UnityEngine.SerializeField;
using ActionAxis = System.Action<UnityEngine.Vector3>;
using ActionPlane = System.Action<UnityEngine.Vector2>;
using ActionValue = System.Action<float>;
using ActionIndex = System.Action<int>;
using ActionState = System.Action<bool>;
using ActionKey = System.Action;
using UnityEngine.InputSystem;
using UnityEngine;

namespace GameProject.Inputs {
    public sealed partial class InputManager : ScriptableObject
    {
        [Header("MOVEMENT INPUTS")]
        [SF, Tooltip("Output: Float Direction")]
        private InputInfo _turn = null;
        
        [SF, Tooltip("Output: Float Direction")]
        private InputInfo _tilt = null;

        [SF, Tooltip("Output: Float Direction")]
        private InputInfo _roll = null;

        [SF, Tooltip("Output: Float Direction")]
        private InputInfo _lean = null;

        [SF, Tooltip("Output: 3D Direction")]
        private InputInfo _move = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _crouch = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _sneak = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _walk = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _run = null;

        [SF, Tooltip("Output: Float Input Duration")]
        private InputInfo _jump = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _dodge = null;

        private Subscription<ActionValue> _onTurn   = null;
        private Subscription<ActionValue> _onTilt   = null;
        private Subscription<ActionValue> _onRoll   = null;
        private Subscription<ActionAxis>  _onMove   = null;
        private Subscription<ActionValue> _onLean   = null;
        private Subscription<ActionKey>   _onCrouch = null;
        private Subscription<ActionKey>   _onSneak  = null;
        private Subscription<ActionKey>   _onWalk   = null;
        private Subscription<ActionKey>   _onRun    = null;
        private Subscription<ActionValue> _onJump   = null;
        private Subscription<ActionKey>   _onDodge  = null;

        private float _jumpTime = 0f;
        private Vector3 _moveDir = Vector3.zero;

// INITIALISATION AND DEINITIALISATION

        /// <summary>
        /// Initialises the movement inputs
        /// </summary>
        private void InitMovement(){
            InitialiseInput<ActionValue>(ref _onTurn, _turn, OnTurnInput);
            InitialiseInput<ActionValue>(ref _onTilt, _tilt, OnTiltInput);
            InitialiseInput<ActionValue>(ref _onRoll, _roll, OnRollInput);
            InitialiseInput<ActionValue>(ref _onLean, _lean, OnLeanInput);

            InitialiseInput<ActionAxis>(ref _onMove, _move, OnMoveInput);
            InitialiseInput<ActionKey>(ref _onCrouch, _crouch, OnCrouchInput);
            InitialiseInput<ActionKey>(ref _onSneak, _sneak, OnSneakInput);
            InitialiseInput<ActionKey>(ref _onWalk, _walk, OnWalkInput);
            InitialiseInput<ActionKey>(ref _onRun, _run, OnRunInput);

            InitialiseInput<ActionValue>(ref _onJump, _jump, OnJumpInput);
            InitialiseInput<ActionKey>(ref _onDodge, _dodge, OnDodgeInput);
        }

        /// <summary>
        /// Deinitialises the movement inputs
        /// </summary>
        private void DeinitMovement(){
            DeinitialiseInput<ActionValue>(ref _onTurn, _turn, OnTurnInput);
            DeinitialiseInput<ActionValue>(ref _onTilt, _tilt, OnTiltInput);
            DeinitialiseInput<ActionValue>(ref _onRoll, _roll, OnRollInput);
            DeinitialiseInput<ActionValue>(ref _onLean, _lean, OnLeanInput);

            DeinitialiseInput<ActionAxis>(ref _onMove, _move, OnMoveInput);
            DeinitialiseInput<ActionKey>(ref _onCrouch, _crouch, OnCrouchInput);
            DeinitialiseInput<ActionKey>(ref _onSneak, _sneak, OnSneakInput);
            DeinitialiseInput<ActionKey>(ref _onWalk, _walk, OnWalkInput);
            DeinitialiseInput<ActionKey>(ref _onRun, _run, OnRunInput);

            DeinitialiseInput<ActionValue>(ref _onJump, _jump, OnJumpInput);
            DeinitialiseInput<ActionKey>(ref _onDodge, _dodge, OnDodgeInput);
        }

// MOVEMENT INPUTS

        /// <summary>
        /// On turn input action callback
        /// <br>Outputs the input direction</br>
        /// </summary>
        private void OnTurnInput(InputAction.CallbackContext context){
            var direction = context.ReadValue<float>();
            _onTurn.NotifySubscribers(direction);
        }

        /// <summary>
        /// On tilt input action callback
        /// <br>Outputs the input direction</br>
        /// </summary>
        private void OnTiltInput(InputAction.CallbackContext context){
            var direction = context.ReadValue<float>();
            _onTilt.NotifySubscribers(direction);
        }

        /// <summary>
        /// On roll input action callback
        /// <br>Outputs the input direction</br>
        /// </summary>
        private void OnRollInput(InputAction.CallbackContext context){
            var direction = context.ReadValue<float>();
            _onRoll.NotifySubscribers(direction);
        }

        /// <summary>
        /// On lean input action callback
        /// <br>Outputs the input direction</br>
        /// </summary>
        private void OnLeanInput(InputAction.CallbackContext context){
            var direction = context.ReadValue<float>();
            _onLean.NotifySubscribers(direction);
        }


        /// <summary>
        /// On move input action callback
        /// <br>Outputs the input direction</br>
        /// </summary>
        private void OnMoveInput(InputAction.CallbackContext context){
            var input = context.ReadValue<Vector2>();

            _moveDir.x = input.x;
            _moveDir.z = input.y;

            _onMove.NotifySubscribers(_moveDir);
        }

        /// <summary>
        /// On crouch input action callback
        /// </summary>
        private void OnCrouchInput(InputAction.CallbackContext context){
            _onCrouch.NotifySubscribers();
        }

        /// <summary>
        /// On sneak input action callback
        /// </summary>
        private void OnSneakInput(InputAction.CallbackContext context){
            _onSneak.NotifySubscribers();
        }

        /// <summary>
        /// On walk input action callback
        /// </summary>
        private void OnWalkInput(InputAction.CallbackContext context){
            _onWalk.NotifySubscribers();
        }

        /// <summary>
        /// On run input action callback
        /// </summary>
        private void OnRunInput(InputAction.CallbackContext context){
            _onRun.NotifySubscribers();
        }


        /// <summary>
        /// On jump input action callback
        /// <br>Outputs the input down duration, on release, in seconds</br>
        /// </summary>
        private void OnJumpInput(InputAction.CallbackContext context){
            if (context.started){
                _jumpTime = (float)context.time;

            } else if (context.performed){
                _onJump.NotifySubscribers(0);

            } else if (context.canceled){
                var duration = (float)context.time - _jumpTime;
                _onJump.NotifySubscribers(duration);
            }
        }

        /// <summary>
        /// On dodge input action callback
        /// </summary>
        private void OnDodgeInput(InputAction.CallbackContext context){
            _onDodge.NotifySubscribers();
        }
    }
}