using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    public class EffectsInteraction : BaseInteraction
    {
        [SF] private ParticleSystem[] _effects = null;

// EFFECT HANDLING

        public override void Show() => Enable();
        public override void Hide() => Disable();
        public override void Enable()  => Enable();
        public override void Disable() => Disable();


        /// <summary>
        /// Pauses the effects
        /// </summary>
        public override void Pause(){
            for (int i = 0; i < _effects.Length; i++){
                _effects[i].Pause(true);
            }
        }

        /// <summary>
        /// Enables the effects
        /// </summary>
        public override void Play(){
            for (int i = 0; i < _effects.Length; i++){
                _effects[i].Play(true);
            }
        }

        /// <summary>
        /// Disables the effects
        /// </summary>
        public override void Stop(){
            for (int i = 0; i < _effects.Length; i++){
                _effects[i].Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    }
}