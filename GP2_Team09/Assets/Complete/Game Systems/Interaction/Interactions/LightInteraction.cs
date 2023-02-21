using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    public class LightInteraction : BaseInteraction
    {
        [SF] private bool _startOff  = false;
        [SF] private AnimationCurve _animation =  null;
        [SF] private UpdateManager _update = null;
        [Space]
        [SF] private Light _light    = null;
        [Space, SF] private Light[] _lights = null;

        private float _time     = 0;
        private float _duration = 0;

        private float   _defValue  = 0;
        private float[] _defValues = null;

// INITIALISATION

        /// <summary>
        /// Initialises the light intensity
        /// </summary>
        private void Awake(){
            var length = _animation.keys.Length - 1;
            var start = _animation[_startOff ? 0 : length];

            if (_light){
                _defValue = _light.intensity;
                _light.intensity = _defValue * start.value;
            }

            _defValues = new float[_lights.Length];

            for (int i = 0; i < _lights.Length; i++){
                var light = _lights[i];

                _defValues[i]   = light.intensity;
                light.intensity = light.intensity * start.value;
            }

            _duration = _animation[length].time;
        }

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

            SetIntensity(_time);
            if (_time < _duration) return;

            _update.Unsubscribe(
                ShowLight, 
                UpdateType.Update
            );
        }


        /// <summary>
        /// Turns off the light
        /// </summary>
        public override void Hide(){
            _time = _duration;
            
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

            SetIntensity(_time);
            if (_time > 0f) return;

            _update.Unsubscribe(
                HideLight, UpdateType.Update
            );
        }


        /// <summary>
        /// Changes the light intensity
        /// </summary>
        private void SetIntensity(float time){
            var strength = _animation.Evaluate(time);
            if (_light) _light.intensity = _defValue * strength;

            for (int i = 0; i < _lights.Length; i++){
                _lights[i].intensity = _defValues[i] * strength;
            }
        }
    }
}