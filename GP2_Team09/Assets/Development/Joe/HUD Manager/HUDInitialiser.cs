using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.HUD
{
    [AddComponentMenu("Managers/HUD Initialiser"),
     DefaultExecutionOrder(-1), DisallowMultipleComponent]
    public sealed class HUDInitialiser : MonoBehaviour
    {
        [SF] private HUDManager _manager = null;
        [Space]
        [SF] private EngineerHUDController _uiEngineer;
        [SF] private CrewHUDController _uiCrew;

        private static HUDInitialiser s_active = null;

// INITIALISATION

        /// <summary>
        /// Initialises the input manager
        /// </summary>
        private void Awake(){
            if (s_active == null) {
                if (_uiCrew) _manager.Initialise(_uiCrew);
                else if (_uiEngineer)_manager.Initialise(_uiEngineer);
                s_active = this;

            } else Destroy(this);
        }

        /// <summary>
        /// Deinitialises the input manager
        /// </summary>
        private void OnDestroy(){
            if (s_active != this) return;
            _manager.OnDestroy();
        }
    }
}