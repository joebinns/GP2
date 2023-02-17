using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Game;

namespace GameProject.Interactions
{
    public class SelectNameInteraction : BaseInteraction
    {
        [SF] private GameManager _game  = null;
        [SF] private ShipNames   _ships = null;

        /// <summary>
        /// Assigns the selected ship name to the game manager
        /// </summary>
        public override void Changed(float value){
            if (value >= 0 && value < _ships.Names.Length)
                _game.SetShipName(_ships.Names[(int)value]);

            else Debug.LogError(new System.ArgumentOutOfRangeException(
                $"{(int)value} is not a valid ship name index"
            ));
        }
    }
}