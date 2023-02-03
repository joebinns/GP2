using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace GameProject.Interactions
{
    public class PatternInteraction : MonoBehaviour
    {
        [SS] public struct StateInfo {
            public GameObject Interactable;
            public bool DesiredState;
        }

        [SF] private StateInfo[] _pattern = null;
        [Space]
        [SF] private UnityEvent _onSuccess = new();
        [SF] private UnityEvent _onChange  = new();

        private Dictionary<GameObject, bool> _states = null;

// INITIALISATION

        /// <summary>
        /// Initialises the pattern states
        /// </summary>
        private void Awake(){
            _states = new Dictionary<GameObject, bool>();

            foreach (var item in _pattern){
                if (!_states.TryAdd(item.Interactable, false)){
                    LogAlreadyAdded(item.Interactable);
                }
            }
        }

// PATTERN HANDLING

        /// <summary>
        /// Updates the interactable's current state
        /// </summary>
        public void UpdateState(GameObject interactable){
            if (_states.TryGetValue(interactable, out var value)){
                _states[interactable] = !value;
                _onChange.Invoke();

            } else LogNotInPattern(interactable);
        }

        /// <summary>
        /// Checks if the interactions matches the defined pattern
        /// </summary>
        public void CheckPattern(){
            var match = true;
            var index = 0;

            foreach (var state in _states.Values){
                if (_pattern[index].DesiredState != state){
                    match = false;
                    break;
                }
                index++;
            }

            if (!match) return;
            _onSuccess.Invoke();
        }

// ERRORS

        /// <summary>
        /// Logs error to console: Multiple of same in pattern, 
        /// </summary>
        private void LogAlreadyAdded(GameObject interactable){
            var msg = "has been assigned to the pattern more than once";
            Debug.LogError($"{interactable.name} {msg}");
        }

        /// <summary>
        /// Logs error to console: Missing interactable in pattern 
        /// </summary>
        private void LogNotInPattern(GameObject interactable){
            var msg = "has not been assigned to the pattern";
            Debug.LogError($"{interactable.name} {msg}");
        }
    }
}