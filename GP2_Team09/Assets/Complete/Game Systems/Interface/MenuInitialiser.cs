using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interface 
{
    [AddComponentMenu("Managers/Menu Initialiser")]
    public class MenuInitialiser : MonoBehaviour
	{
		[SF] private MenuManager _manager = null;
        [SF] private Canvas _uiMainMenu = null;
        [SF] private Canvas _uiPauseMenu = null;

        private static MenuInitialiser s_active = null;

        private void Awake(){
            if (s_active == null && _uiMainMenu != null && _uiPauseMenu != null){
                _manager.Initialise(_uiMainMenu, _uiPauseMenu);
                s_active = this;
            
            } else {
                _manager.SetCursor(
                    _manager.MenuOpened || 
                    _manager.PauseOpened
                );
                Destroy(this);
            }
        }

		private void OnDestroy(){
			if (s_active != this) return;
            _manager.OnDestroy();
        }
	}
}