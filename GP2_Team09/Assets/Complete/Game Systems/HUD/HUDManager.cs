using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Inputs;
using GameProject.Interface;

namespace GameProject.HUD
{
    [CreateAssetMenu(fileName = "HUD Manager", menuName = "Managers/HUD")]
    public sealed partial class HUDManager : ScriptableObject
    {
        [SF] private InputManager _input = null;
        [SF] private MenuManager _menu = null;

        private EngineerHUDController _engineer;
        private CrewHUDController _crew;
        private HUDController _current = null;

// INITIALISATION

        /// <summary>
        /// Initialises the engineer HUD manager
        /// </summary>
        public void Initialise(EngineerHUDController engineer) {
            _input.SubscribeKey(OnPauseInput, InputType.Pause);
            _engineer = engineer;
        }
        /// <summary> 
        /// Initialises the crew HUD manager
        /// </summary>
        public void Initialise(CrewHUDController crew) {
            _input.SubscribeKey(OnPauseInput, InputType.Pause);
            _crew = crew;
        }

        /// <summary>
        /// Deinitialise the HUD manager
        /// </summary>
        public void OnDestroy(){
            _input.UnsubscribeKey(OnPauseInput, InputType.Pause);
        }

// INPUT HANDLING

        /// <summary>
        /// Toggle pause menu
        /// </summary>
        public void OnPauseInput(){
            _current.gameObject.SetActive(
                !_menu.Opened
            );
        }

// HUD Handling

        /// <summary>
        /// Display win HUD based on parameter
        /// </summary>
        public void DisplayWon(bool display) {
            _crew.DisplayWon(display);
        }
        
        /// <summary>
        /// Display lose HUD based on parameter
        /// </summary>
        public void DisplayLost(bool display) {
            _crew.DisplayLost(display);
        }
    }
}