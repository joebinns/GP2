using SF = UnityEngine.SerializeField;
using UnityEngine.EventSystems;
using UnityEngine;
using GameProject.Inputs;

namespace GameProject.Interface
{
    [CreateAssetMenu(fileName = "Menu Manager", menuName = "Managers/Menu")]
    public sealed class MenuManager : ScriptableObject
	{
		[SF] private InputManager _input = null;

        private Canvas _uiMainMenu = null;
        private Canvas _uiPauseMenu = null;
        private UIResetState _uiMainReset  = null;
        private UIResetState _uiPauseReset = null;

// PROPERTIES

        public bool MenuOpened {
            get {
                if (_uiMainMenu == null) 
                    return false;
                
                return _uiMainMenu.enabled;
            }
        }
        public bool PauseOpened {
            get {
                if (_uiPauseMenu == null) 
                    return false;
                
                return _uiPauseMenu.enabled;
            }
        }
        public Canvas MainMenu => _uiMainMenu;
        public Canvas PauseMenu => _uiPauseMenu;

// INITIALISATION AND FINALISATION

        /// <summary>
        /// Initialises the menu manager
        /// </summary>
        public void Initialise(Canvas mainmenu, Canvas pausemenu){
            _uiMainMenu  = mainmenu;
            _uiPauseMenu = pausemenu;

            if (_uiMainMenu  == null ||
                _uiPauseMenu == null) 
                return;

            _input.SubscribeKey(
                OnPauseToggleInput,
                InputType.Pause
            );
            
            _uiMainReset  = _uiMainMenu.GetComponent<UIResetState>();
            _uiPauseReset = _uiPauseMenu.GetComponent<UIResetState>();
            SetCursor(_uiMainMenu.enabled);
        }

        /// <summary>
        /// Finalises this menu manager
        /// </summary>
        public void OnDestroy(){
            _input.UnsubscribeKey(
                OnPauseToggleInput, 
                InputType.Pause
            );
        }

// INPUT HANDLING

        /// <summary>
        /// On input manager, toggle menu input event
        /// </summary>
        private void OnPauseToggleInput() {
            if (_uiMainMenu.enabled) return;
            ShowPauseMenu(!_uiPauseMenu.enabled);
        }

// MENU MANAGEMENT

        /// <summary>
        /// Changes the cursors visibility and lock state
        /// </summary>
        public void SetCursor(bool visible){
            Cursor.visible = visible;

            Cursor.lockState = visible ?
                CursorLockMode.None :
                CursorLockMode.Locked;
        }

        /// <summary>
        /// Shows or hides the main menu
        /// </summary>
        public void ShowMainMenu(bool show){
            if (_uiMainMenu == null) return;

            _uiMainReset.ResetInterface();
            _uiPauseMenu.gameObject.SetActive(!show);
            _uiMainMenu.gameObject.SetActive(true);
            _uiMainMenu.enabled = show;

            if (!show){
                var es = EventSystem.current;
                es.SetSelectedGameObject(null);
            }
            
            _input.SetInputStates(_input.ActionGroup, !show);
            _input.SetInputStates(_input.InterfaceGroup, !show);
            _input.SetInputStates(_input.MovementGroup, !show);

            SetCursor(show);
        }

        /// <summary>
        /// Shows or hides the pause menu
        /// </summary>
        public void ShowPauseMenu(bool show){
            if (_uiPauseMenu == null) return;

            _uiMainReset.ResetInterface();
            _uiPauseMenu.enabled = show;

            if (!show){
                var es = EventSystem.current;
                es.SetSelectedGameObject(null);
            }
            
            _input.SetInputStates(_input.ActionGroup, !show);
            _input.SetInputStates(_input.InterfaceGroup, !show);
            _input.SetInputStates(_input.MovementGroup, !show);

            SetCursor(show);
        }

// MENU EVENTS

        /// <summary>
        /// Quits the game on camera look at quit event
        /// </summary>
        public void QuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
	}
}