using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Updates;
using System.Collections.Generic;

namespace GameProject.Interactions
{
    public class LightInteraction : BaseInteraction
    {
        [Header("General Settings")]
        [SF] private bool _startOff  = false;
        [SF] private AnimationCurve _animation =  null;
        [SF] private UpdateManager _update = null;

        [Header("Colour Settings")]
        [SF] private bool _changeColour = false;
        [SF] private Color _offColour = Color.black;
        [SF] private Color _onColour  = Color.white;

        [Header("Intensity Settings")]
        [SF] private bool _changeIntensity = true;

        [Header("Light References")]
        [SF] private Light _light    = null;
        [SF] private Light[] _lights = null;
        [Space, SF] private List<ActionInfo> _onShow = null;
        [Space, SF] private List<ActionInfo> _onHide = null;

        private float _time     = 0;
        private float _duration = 0;

        private float _defIntensity  = 0;
        private Color _defColour = Color.clear;

        private float[] _defIntensities = null;
        private Color[] _defColours     = null;

// INITIALISATION

        /// <summary>
        /// Initialises the light intensity
        /// </summary>
        private void Start(){
            if (_animation.keys.Length < 2) return;
            var length = _animation.keys.Length - 1;
            var start  = _animation[_startOff ? 0 : length];

            InitLight(start.value);
            InitLights(start.value);

            _duration = _animation[length].time;
        }

        /// <summary>
        /// Initialises the light
        /// </summary>
        private void InitLight(float strength){
            if (!_light) return;

            if (_changeColour){
                _defColour = _light.color;
                SetColour(_light, strength);
            }

            if (_changeIntensity){
                _defIntensity = _light.intensity;
                SetIntensity(_light, _defIntensity, strength);
            }
        }

        /// <summary>
        /// Initialises the lights
        /// </summary>
        private void InitLights(float strength){
            if (_changeColour)
                _defColours = new Color[_lights.Length];

            if (_changeIntensity)
                _defIntensities = new float[_lights.Length];

            for (int i = 0; i < _lights.Length; i++){
                var light = _lights[i];
                if (!light) continue;

                if (_changeColour){
                    _defColours[i] = light.color;
                    SetColour(light, strength);
                }

                if (_changeIntensity){
                    _defIntensities[i] = light.intensity;
                    SetIntensity(light, light.intensity, strength);
                }
            }
        }

// LIGHT HANDLING

        /// <summary>
        /// Resets the light properties
        /// </summary>
        public override void Restore(){
            if (_light){
                if (_changeColour)
                    _light.color = _defColour;

                if (_changeIntensity)
                    _light.intensity = _defIntensity;
            }

            for (int i = 0; i < _lights.Length; i++){
                var light = _lights[i];
                if (!light) continue;

                if (_changeColour)
                    light.color = _defColours[i];

                if (_changeIntensity)
                    light.intensity = _defIntensities[i];
            }
        }


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

            UpdateLights(_time);
            if (_time < _duration) return;

            _update.Unsubscribe(
                ShowLight, 
                UpdateType.Update
            );

            Interact(_onShow);
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

            UpdateLights(_time);
            if (_time > 0f) return;

            _update.Unsubscribe(
                HideLight, UpdateType.Update
            );

            Interact(_onHide);
        }


        /// <summary>
        /// Changes the lights properties
        /// </summary>
        private void UpdateLights(float time){
            var strength = _animation.Evaluate(time);
            
            if (_light){
                if (_changeColour)
                    SetColour(_light, strength);

                if (_changeIntensity)
                    SetIntensity(_light, _defIntensity, strength);
            }
            
            for (int i = 0; i < _lights.Length; i++){
                var light = _lights[i];
                if (!light) continue;

                if (_changeColour)
                    SetColour(light, strength);

                if (_changeIntensity)
                    SetIntensity(light, _defIntensities[i], strength);
            }
        }

        /// <summary>
        /// Changes the light colour
        /// </summary>
        private void SetColour(Light light, float strength){
            light.color = Color.Lerp(_offColour, _onColour, strength);
        }

        /// <summary>
        /// Changes the light intensity
        /// </summary>
        private void SetIntensity(Light light, float intensity, float strength){
            light.intensity = intensity * strength;
        }
    }
}