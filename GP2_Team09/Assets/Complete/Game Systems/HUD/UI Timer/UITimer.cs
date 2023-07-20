using GameProject.Interactions;
using UnityEngine;
using TMPro;

namespace GameProject.HUD
{
    public class UITimer : TimerInteraction
    {
        [SerializeField] private TMP_Text _text;

// TIMER HANDLING

        public void OverrideThreshold(float threshold) {
            SetThreshold(threshold);
        }

        protected override void OnUpdateTimer(float deltaTime){
            base.OnUpdateTimer(deltaTime);

            var formattedTime = FormatTime(_time);
            _text.text = formattedTime;
        }

        private string FormatTime(float time) => 
            System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
    }
}
