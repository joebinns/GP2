using SF = UnityEngine.SerializeField;
using ActionAxis = System.Action<UnityEngine.Vector3>;
using ActionPlane = System.Action<UnityEngine.Vector2>;
using ActionValue = System.Action<float>;
using ActionIndex = System.Action<int>;
using ActionState = System.Action<bool>;
using ActionKey = System.Action;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Globalization;

namespace GameProject.Inputs {
    public sealed partial class InputManager : ScriptableObject
    {
        [Header("VARIOUS INPUTS")]
        [SF, Tooltip("Output: Nothing")]
        private InputInfo _pause = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _save = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _load = null;

        [SF, Tooltip("Output: Nothing")]
        private InputInfo _screenshot = null;

        private Subscription<ActionKey> _onPause = null;
        private Subscription<ActionKey> _onSave = null;
        private Subscription<ActionKey> _onLoad = null;
        private Subscription<ActionKey> _onScreenshot = null;

// INITIALISATION AND DENITIALISATION

        /// <summary>
        /// Initialises the other various inputs
        /// </summary>
        private void InitOther(){
            InitialiseInput<ActionKey>(ref _onPause, _pause, OnPauseInput);
            InitialiseInput<ActionKey>(ref _onSave, _save, OnSaveInput);
            InitialiseInput<ActionKey>(ref _onLoad, _load, OnLoadInput);
            InitialiseInput<ActionKey>(ref _onScreenshot, _screenshot, OnScreenshotInput);
        }

        /// <summary>
        /// Deinitialises the other various inputs
        /// </summary>
        private void DeinitOther(){
            DeinitialiseInput<ActionKey>(ref _onPause, _pause, OnPauseInput);
            DeinitialiseInput<ActionKey>(ref _onSave, _save, OnSaveInput);
            DeinitialiseInput<ActionKey>(ref _onLoad, _load, OnLoadInput);
            DeinitialiseInput<ActionKey>(ref _onScreenshot, _screenshot, OnScreenshotInput);
        }

// MOVEMENT INPUTS

        /// <summary>
        /// On pause input action callback
        /// </summary>
        private void OnPauseInput(InputAction.CallbackContext context){
            _onPause.NotifySubscribers();
        }

        /// <summary>
        /// On save input action callback
        /// </summary>
        private void OnSaveInput(InputAction.CallbackContext context){
            _onSave.NotifySubscribers();
        }

        /// <summary>
        /// On load input action callback
        /// </summary>
        private void OnLoadInput(InputAction.CallbackContext context){
            _onLoad.NotifySubscribers();
        }

        /// <summary>
        /// On screenshot input action callback
        /// </summary>
        private void OnScreenshotInput(InputAction.CallbackContext context){
            _onScreenshot.NotifySubscribers();
        }
    }
}