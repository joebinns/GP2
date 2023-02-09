using SF = UnityEngine.SerializeField;
using System;
using GameProject.Interactions;
using TMPro;
using UnityEngine;

namespace GameProject.HUD
{
    public class UITimer : TimerInteraction
    {
        [SF] private TMP_Text _text;

        private void Start() {
            Begin(); // TODO: Run this when players are ready
        }

        public void OverrideThreshold(float threshold) {
            SetThreshold(threshold);
        }

        protected override void OnUpdateTimer(float deltaTime)
        {
            base.OnUpdateTimer(deltaTime);
            var formattedTime = FormatTime(_time);
            _text.text = formattedTime;
        }

        private string FormatTime(float time) => TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
    }
}
