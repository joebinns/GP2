using UnityEngine;
using SF = UnityEngine.SerializeField;

namespace GameProject.HUD
{
    public class CrewHUDController : HUDController
    {
        [SF] private GameObject _uiLost;
        [SF] private GameObject _uiWon;

        /// <summary>
        /// Display win HUD based on parameter
        /// </summary>
        public void DisplayWon(bool display) {
            _uiWon.SetActive(display);
        }
        
        /// <summary>
        /// Display lose HUD based on parameter
        /// </summary>
        public void DisplayLost(bool display) {
            _uiLost.SetActive(display);
        }
    }
}
