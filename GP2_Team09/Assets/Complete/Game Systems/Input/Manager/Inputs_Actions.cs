using SF = UnityEngine.SerializeField;
using ActionAxis  = System.Action<UnityEngine.Vector3>;
using ActionPlane = System.Action<UnityEngine.Vector2>;
using ActionValue = System.Action<float>;
using ActionIndex = System.Action<int>;
using ActionState = System.Action<bool>;
using ActionKey   = System.Action;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem;
using UnityEngine;

namespace GameProject.Inputs {
    public sealed partial class InputManager : ScriptableObject
    {
        [Header("ACTION INPUTS")]
        [SF, Tooltip("Output: 2D Position or Direction")]
        private InputInfo _cursor = null;

        [SF, Tooltip("Output: 2D Position or Direction")]
        private InputInfo _pointer = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _use = null;

        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _primary = null;

        [SF, Tooltip("Output: Nothing")] 
        private InputInfo _secondary = null;

        [SF, Tooltip("Output: Integer Number or Direction")]
        private InputInfo _switch = null;

        private Subscription<ActionPlane> _onCursor    = null;
        private Subscription<ActionPlane> _onPointer   = null;
        private Subscription<ActionKey>   _onUse       = null;
        private Subscription<ActionKey>   _onPrimary   = null;
        private Subscription<ActionKey>   _onSecondary = null;
        private Subscription<ActionIndex> _onSwitch    = null;

// INITIALISATION AND DENITIALISATION

        /// <summary>
        /// Initialises the action inputs
        /// </summary>
        private void InitActions(){
            InitialiseInput<ActionPlane>(ref _onCursor, _cursor, OnCursorInput);
            InitialiseInput<ActionPlane>(ref _onPointer, _pointer, OnPointerInput);
            InitialiseInput<ActionKey>(ref _onUse, _use, OnUseInput);
            InitialiseInput<ActionKey>(ref _onPrimary, _primary, OnPrimaryInput);
            InitialiseInput<ActionKey>(ref _onSecondary, _secondary, OnSecondaryInput);
            InitialiseInput<ActionIndex>(ref _onSwitch, _switch, OnSwitchInput);
        }

        /// <summary>
        /// Deinitialises the action inputs
        /// </summary>
        private void DeinitActions(){
            DeinitialiseInput<ActionPlane>(ref _onCursor, _cursor, OnCursorInput);
            DeinitialiseInput<ActionPlane>(ref _onPointer, _pointer, OnPointerInput);
            DeinitialiseInput<ActionKey>(ref _onUse, _use, OnUseInput);
            DeinitialiseInput<ActionKey>(ref _onPrimary, _primary, OnPrimaryInput);
            DeinitialiseInput<ActionKey>(ref _onSecondary, _secondary, OnSecondaryInput);
            DeinitialiseInput<ActionIndex>(ref _onSwitch, _switch, OnSwitchInput);
        }

// ACTION INPUTS

        /// <summary>
        /// On cursor input action callback
        /// </summary>
        private void OnCursorInput(InputAction.CallbackContext context){
            var position = context.ReadValue<Vector2>();
            _onCursor.NotifySubscribers(position);
        }

        /// <summary>
        /// On pointer input action callback
        /// </summary>
        private void OnPointerInput(InputAction.CallbackContext context){
            var direction = context.ReadValue<Vector2>();
            _onPointer.NotifySubscribers(direction);
        }

        /// <summary>
        /// On use input action callback
        /// </summary>
        private void OnUseInput(InputAction.CallbackContext context){
            _onUse.NotifySubscribers();
        }

        /// <summary>
        /// On primary input action callback
        /// </summary>
        private void OnPrimaryInput(InputAction.CallbackContext context){
            _onPrimary.NotifySubscribers();
        }

        /// <summary>
        /// On secondary input action callback
        /// </summary>
        private void OnSecondaryInput(InputAction.CallbackContext context){
            _onSecondary.NotifySubscribers();
        }

        /// <summary>
        /// On switch input action callback
        /// <br>Outputs either the selected number or the switch direction</br>
        /// </summary>
        private void OnSwitchInput(InputAction.CallbackContext context){
            var control = (KeyControl)context.control;
            var key = control.keyCode.ToString();

            if (int.TryParse(key, out var value)){
                _onSwitch.NotifySubscribers(value);

            } else {
                var direction = context.ReadValue<float>();
                _onSwitch.NotifySubscribers((int)direction);
            }
        }
    }
}