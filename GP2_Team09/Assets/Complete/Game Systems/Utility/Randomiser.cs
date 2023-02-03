using Random = System.Random;
using DateTime = System.DateTime;
using UnityEngine;

namespace GameProject {
    public sealed class Randomise
    {
        private Random _random = null;

// INITIALISATION

        /// <summary>
        /// Initialises with DateTime seed
        /// </summary>
        public Randomise(){
            var date = DateTime.Now;
            var seed = $"{date.Month}{date.Day}{date.Hour}{date.Minute}";
            Initialise(int.Parse(seed));
        }

        /// <summary>
        /// Initialises with custom seed
        /// </summary>
        public Randomise(string seed){
            Initialise(seed.GetHashCode());
        }

        /// <summary>
        /// Initialises with custom seed
        /// </summary>
        public Randomise(int seed){
            Initialise(seed);
        }

        /// <summary>
        /// Initialises with speed
        /// </summary>
        private void Initialise(int seed){
            _random = new Random(seed);
        }

// RANDOMISATION

        /// <summary>
        /// Returns a value greater or equal to min and less than max
        /// </summary>
        public int Value(int min, int max){
            return _random.Next(min, max);
        }

        /// <summary>
        /// Returns a value greater or equal to min and less than max
        /// </summary>
        public float Value(float min, float max){
            return Mathf.Lerp(min, max, (float)_random.NextDouble());
        }
    }
}