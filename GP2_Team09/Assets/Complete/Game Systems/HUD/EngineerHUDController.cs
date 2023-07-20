using UnityEngine;

namespace GameProject.HUD
{
    public class EngineerHUDController : HUDController
    {
        [SerializeField] private GameObject _uiLost = null;
        [SerializeField] private GameObject _uiWon = null;

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
