using SF = UnityEngine.SerializeField;
using Random = System.Random;
using UnityEngine;
using GameProject.Interactions;

namespace GameProject.Game
{
    public class RandomManager : BaseInteraction
    {
        [SF] private GameManager _game = null;

        private Random _random = null;

// RANDOM HANDLING

        /// <summary>
        /// Initialises the random generator
        /// </summary>
        public override void Trigger(){
            if (!string.IsNullOrEmpty(_game.ShipName))
                _random = new Random(GetSeed());
        }

        /// <summary>
        /// Returns the ship name converted into a seed
        /// </summary>
        private int GetSeed(){
            var name = _game.ShipName;
            var seed = string.Empty;

            for (int i = 0; i < name.Length; i++)
                seed = string.Concat(seed, (int)name[i]);

            return int.Parse(seed);
        }
    }
}