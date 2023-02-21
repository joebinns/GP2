using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    public class ValueInteraction : BaseInteraction
    {
        [SS] public struct ValueInfo {
            public BaseInteraction Interactable;
            public int DesiredValue;
        }

        [SF] private int _minValue = 0;
        [SF] private int _maxValue = 10;

        [Space, SF] private ValueInfo[] _pattern = null;
        [Space, SF] private List<ActionInfo> _onSuccess = null;
        [Space, SF] private List<ActionInfo> _onFailure = null;
        [Space, SF] private List<ActionInfo> _onChange  = null;

        private Dictionary<BaseInteraction, int> _states = null;

// INITIALISATION

        /// <summary>
        /// Initialises the pattern states
        /// </summary>
        private void Awake(){
            _states = new Dictionary<BaseInteraction, int>();

            foreach (var info in _pattern){
                var value = GetValue(info.Interactable);

                if (_states.TryAdd(info.Interactable, value)){
                    Interact(_onChange, info.Interactable, value);

                } else LogAlreadyAdded(info.Interactable);
            }
        }

// PATTERN HANDLING

        /// <summary>
        /// Updates the interactable's current value
        /// </summary>
        public override void Compare(BaseInteraction interactable){
            if (_states.TryGetValue(interactable, out var value)){
                value = GetValue(interactable);
                
                _states[interactable] = value;
                Interact(_onChange, interactable, value);

            } else LogNotInPattern(interactable);
        }

        /// <summary>
        /// Checks if the interactions matches the defined pattern
        /// </summary>
        public override void CheckResult(){
            var match = true;
            var index = 0;

            foreach (var value in _states.Values){
                if (_pattern[index].DesiredValue != value){
                    match = false;
                    break;
                }
                index++;
            }

            if (match) Interact(_onSuccess);
            else Interact(_onFailure);
        }

        /// <summary>
        /// Returns the rotation between min/max value
        /// </summary>
        private int GetValue(BaseInteraction interactable) {
            var grabInteractable = interactable as TorsionalGrabInteraction;
            var lerp = Mathf.InverseLerp(grabInteractable.MinAngle, grabInteractable.MaxAngle, grabInteractable.Angle);
            var value = Mathf.Lerp(_maxValue, _minValue, lerp);
            return Mathf.RoundToInt(value);
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
        /// Returns the trigger action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onSuccess,
                _onFailure,
                _onChange,
            };
        }

        /// <summary>
        /// Assigns the value actions
        /// </summary>
        public override void SetActions(List<List<ActionInfo>> actions){
            _onSuccess = actions[0];
            _onFailure = actions[1];
            _onChange  = actions[2];
        }
    }
}