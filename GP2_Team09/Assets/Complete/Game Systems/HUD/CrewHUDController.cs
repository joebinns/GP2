using SF = UnityEngine.SerializeField;
using UnityEngine;
using TMPro;

namespace GameProject.HUD
{
    public class CrewHUDController : HUDController
    {
        [Header("Crew Settings")]
        [SF] private TMP_Text   _health = null;
        [SF] private UITimer    _timer  = null;
        [SF] private GameObject _uiLost = null;
        [SF] private GameObject _uiWon  = null;

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartTimer(){
            if (!_timer) return;
            _timer.Begin();
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StopTimer(){
            if (!_timer) return;
            _timer.End();
        }

        /// <summary>
        /// Changes the health text
        /// </summary>
        public void UpdateHealth(int health){
            if (!_health) return;
            _health.text = $"Lives: {health}";
        }

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