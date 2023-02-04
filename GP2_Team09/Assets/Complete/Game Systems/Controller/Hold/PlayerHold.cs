using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Hold
{
    public class PlayerHold : MonoBehaviour
    {
        [SF] private Transform _holdPivot;

        public bool IsHolding;
        
        public Transform HoldPivot => _holdPivot;

        /// <summary>
        /// Initialise the player hold and hold pivot references on all holdable game objects
        /// </summary>
        private void Start() {
            foreach (var interaction in FindObjectsOfType<HoldInteraction>()) {
                interaction.PlayerHold = this;
                interaction.HoldPivot = _holdPivot;
            }
        }
    }
}
