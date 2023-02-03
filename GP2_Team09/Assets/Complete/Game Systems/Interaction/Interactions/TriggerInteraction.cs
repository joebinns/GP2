using SF = UnityEngine.SerializeField;
using UnityEngine.Events;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class TriggerInteraction : MonoBehaviour
    {
        [SF] private LayerMask _playerLayer = 1 << 0;
        [Space]
        [SF] private UnityEvent _onEntered = new();
        [SF] private UnityEvent _onExited  = new();

// TRIGGER HANDLING

        /// <summary>
        /// On player entering trigger
        /// </summary>
        private void OnTriggerEnter(Collider other){
            var layer = other.gameObject.layer;

            if (((1 << layer) & _playerLayer) != 0){
                _onEntered.Invoke();
            }
        }

        /// <summary>
        /// On player exiting trigger
        /// </summary>
        private void OnTriggerExit(Collider other){
            var layer = other.gameObject.layer;

            if (((1 << layer) & _playerLayer) != 0){
                _onExited.Invoke();
            }
        }
    }
}