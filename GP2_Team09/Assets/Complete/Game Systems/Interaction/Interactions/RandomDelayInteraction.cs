using SF = UnityEngine.SerializeField;
using Random = System.Random;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    public class RandomDelayInteraction : DelayInteraction
    {
        [Header("Randomisation")]
        [SF] private ChaosSettings _settings = null;
        
        private Random _random = null;

// INITIALISATION

        /// <summary>
        /// Initialises the number generator
        /// </summary>
        private void Awake(){
            var seed = Time.time.GetHashCode();
            _random = new Random(seed);
        }

// TIMER HANDLING

        /// <summary>
        /// Starts the timer
        /// </summary>
        public override void Begin(){
            base.Begin();

            var value = (float)_random.NextDouble();
            var range = _settings.RandomLightTimer;

            _time = Mathf.Lerp(range.x, range.y, value);
        }
    }
}