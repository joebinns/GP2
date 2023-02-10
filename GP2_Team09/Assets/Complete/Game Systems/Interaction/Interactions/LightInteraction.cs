using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    public class LightInteraction : BaseInteraction
    {
        [SF] private Light _light  = null;
        [SF] private AnimationCurve _animation =  null;
        [Space, SF] private UpdateManager _update = null;

        private float _time = 0;

// LIGHT HANDLING

        /// <summary>
        /// Turns on the light
        /// </summary>
        public override void Show(){
            _time = 0;

            _update.Subscribe(
                ShowLight,
                UpdateType.Update
            );
        }

        /// <summary>
        /// Animates the light
        /// </summary>
        private void ShowLight(float deltaTime){
            _time += deltaTime;

            _light.intensity = _animation.Evaluate(_time);
            if (_time < 1f) return;

            _update.Unsubscribe(ShowLight, UpdateType.Update);
        }


        /// <summary>
        /// Turns off the light
        /// </summary>
        public override void Hide(){
            _time = 1;
            
            _update.Subscribe(
                HideLight,
                UpdateType.Update
            );
        }

        /// <summary>
        /// Animates the light
        /// </summary>
        private void HideLight(float deltaTime){
            _time -= deltaTime;
            
            _light.intensity = _animation.Evaluate(_time);
            if (_time > 0f) return;

            _update.Unsubscribe(HideLight, UpdateType.Update);
        }
    }
}