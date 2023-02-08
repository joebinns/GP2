using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class TriggerInteraction : BaseInteraction
    {
        [SF] private LayerMask _playerLayer = 1 << 0;
        [Space, SF] private List<ActionInfo> _onEntered = null;
        [Space, SF] private List<ActionInfo> _onExited  = null;

// TRIGGER HANDLING

        /// <summary>
        /// On player entering trigger
        /// </summary>
        private void OnTriggerEnter(Collider other){
            var layer = other.gameObject.layer;

            if (((1 << layer) & _playerLayer) != 0){
                Interact(_onEntered);
            }
        }

        /// <summary>
        /// On player exiting trigger
        /// </summary>
        private void OnTriggerExit(Collider other){
            var layer = other.gameObject.layer;

            if (((1 << layer) & _playerLayer) != 0){
                Interact(_onExited);
            }
        }

// DATA HANDLING

        /// <summary>
        /// Returns the trigger action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onEntered,
                _onExited,
            };
        }
    }
}