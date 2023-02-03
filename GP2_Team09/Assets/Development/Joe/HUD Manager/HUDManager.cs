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
        /// Initialises the game manager
        /// </summary>
        public void Initialise(EngineerHUDController engineer, CrewHUDController crew) {
            _input.SubscribeKey(OnPauseInput, InputType.Pause);
            _engineer = engineer;
            _crew = crew;
        }

        /// <summary>
        /// Deinitialise the game manager
        /// </summary>
        public void OnDestroy(){
            _input.UnsubscribeKey(OnPauseInput, InputType.Pause);
        }

// INPUT HANDLING

        /// <summary>
        /// 
        /// </summary>
        public void OnPauseInput(){
            Debug.Log("Input");
            _current.gameObject.SetActive(
                !_menu.Opened
            );
        }

// HUD Handling

        /// <summary>
        /// Display only engineer HUD based on parameter
        /// </summary>
        public void DisplayEngineer(bool display){
            //DisplayCrew(!display); caused infinite loop
            _crew.gameObject.SetActive(!display);
            _engineer.gameObject.SetActive(display);
            _current = _engineer;
        }
        
        /// <summary>
        /// Display only crew HUD based on parameter
        /// </summary>
        public void DisplayCrew(bool display){
            //DisplayEngineer(!display); caused infinite loop
            _engineer.gameObject.SetActive(!display);
            _crew.gameObject.SetActive(display);
            _current = _crew;
        }

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