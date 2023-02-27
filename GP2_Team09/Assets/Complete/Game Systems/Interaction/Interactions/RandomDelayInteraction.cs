using SF = UnityEngine.SerializeField;
using Random = System.Random;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    public class RandomDelayInteraction : DelayInteraction
    {
        [Header("Randomisation")]
        [SF] private Vector2 _minMaxDelay = new Vector2(30, 60);
        
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

            _time = Mathf.Lerp(
                _minMaxDelay.x, 
                _minMaxDelay.y,
                (float)_random.NextDouble()
            );
        }
    }
}