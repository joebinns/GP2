using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

namespace GameProject.Interactions
{
    public class PatternInteraction : BaseInteraction
    {
        [SS] public struct StateInfo {
            public BaseInteraction Target;
            public bool DesiredState;
        }

        [SF] private StateInfo[] _pattern = null;
        [Space, SF] private List<ActionInfo> _onSuccess = null;
        [Space, SF] private List<ActionInfo> _onFailure = null;
        [Space, SF] private List<ActionInfo> _onChange  = null;

        private Dictionary<BaseInteraction, bool> _states = null;

// INITIALISATION

        /// <summary>
        /// Initialises the pattern states
        /// </summary>
        private void Awake(){
            _states = new Dictionary<BaseInteraction, bool>();

            foreach (var info in _pattern){
                if (_states.TryAdd(info.Target, false)){
                    Interact(_onChange, info.Target, false);

                } else LogAlreadyAdded(info.Target);
            }
        }

// PATTERN HANDLING

        /// <summary>
        /// Updates the interactable's current state
        /// </summary>
        public override void Compare(BaseInteraction interactable){
            if (_states.TryGetValue(interactable, out var value)){
                _states[interactable] = !value;           
                Interact(_onChange, interactable, value);

            } else LogNotInPattern(interactable);
        }

        /// <summary>
        /// Checks if the interactions matches the defined pattern
        /// </summary>
        public override void CheckResult(){
            var match = true;
            var index = 0;

            foreach (var state in _states.Values){
                if (_pattern[index].DesiredState != state){
                    match = false;
                    break;
                }
                index++;
            }

            if (match) Interact(_onSuccess);
            else Interact(_onFailure);
        }

// ERRORS

        /// <summary>
        /// Logs error to console: Multiple of same in pattern, 
        /// </summary>
        private void LogAlreadyAdded(BaseInteraction interactable){
            var msg = "has been assigned to the pattern more than once";
            Debug.LogError($"{interactable.gameObject.name} {msg}");
        }

        /// <summary>
        /// Logs error to console: Missing interactable in pattern 
        /// </summary>
        private void LogNotInPattern(BaseInteraction interactable){
            var msg = "has not been assigned to the pattern";
            Debug.LogError($"{interactable.gameObject.name} {msg}");
        }

// DATA HANDLING

        /// <summary>
        /// Returns the pattern action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onSuccess,
                _onFailure,
                _onChange,
            };
        }

        /// <summary>
        /// Assigns the pattern actions
        /// </summary>
        public override void SetActions(List<List<ActionInfo>> actions){
            _onSuccess = actions[0];
            _onFailure = actions[1];
            _onChange  = actions[2];
        }
    }
}