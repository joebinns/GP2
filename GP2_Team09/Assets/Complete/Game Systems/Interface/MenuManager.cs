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

// PROPERTIES

        public bool Opened {
            get {
                if (_uiMainMenu == null) 
                    return false;
                
                return _uiMainMenu.enabled;
            }
        }
        public Canvas MainMenu => _uiMainMenu;

// INITIALISATION AND FINALISATION

        /// <summary>
        /// Initialises the menu manager
        /// </summary>
        public void Initialise(Canvas mainmenu){
            _uiMainMenu = mainmenu;
            if (_uiMainMenu == null) return;
            
            _input.SubscribeKey(
                OnMenuToggleInput, 
                InputType.Pause
            );

            SetCursor(_uiMainMenu.enabled);
        }

        /// <summary>
        /// Finalises this menu manager
        /// </summary>
        public void OnDestroy(){
            _input.UnsubscribeKey(
                OnMenuToggleInput, 
                InputType.Pause
            );
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
        public void ShowMenu(bool show){
            if (_uiMainMenu == null) return;

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
        /// On input manager, toggle menu input event
        /// </summary>
        private void OnMenuToggleInput() {
            ShowMenu(!_uiMainMenu.enabled);
        }

// MENU EVENTS

        /// <summary>
        /// Quits the game on camera look at quit event
        /// </summary>
        public void OnQuitGame(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
        }
	}
}