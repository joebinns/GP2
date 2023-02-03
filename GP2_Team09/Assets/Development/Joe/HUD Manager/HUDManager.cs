using UnityEngine;

namespace GameProject.HUD
{
    [CreateAssetMenu(fileName = "HUD Manager",
     menuName = "Managers/HUD")]
    public sealed partial class HUDManager : ScriptableObject
    {
        private EngineerHUDController _engineer;
        private CrewHUDController _crew;

// INITIALISATION

        /// <summary>
        /// Initialises the game manager
        /// </summary>
        public void Initialise(EngineerHUDController engineer, CrewHUDController crew) {
            _engineer = engineer;
            _crew = crew;
        }

// DEINITIALISATION

        /// <summary>
        /// Deinitialise the game manager
        /// </summary>
        public void OnDestroy() {
        }
        
// HUD Handling

        /// <summary>
        /// Display only engineer HUD based on parameter
        /// </summary>
        public void DisplayEngineer(bool display) {
            //DisplayCrew(!display); caused infinite loop
            _crew.gameObject.SetActive(!display);
            _engineer.gameObject.SetActive(display);
        }
        
        /// <summary>
        /// Display only crew HUD based on parameter
        /// </summary>
        public void DisplayCrew(bool display) {
            //DisplayEngineer(!display); caused infinite loop
            _engineer.gameObject.SetActive(!display);
            _crew.gameObject.SetActive(display);
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