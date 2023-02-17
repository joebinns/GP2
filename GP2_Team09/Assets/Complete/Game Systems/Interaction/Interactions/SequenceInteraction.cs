using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    public class SequenceInteraction : BaseInteraction
    {
        [SF] private GameObject[] _sequence = null;
        [Space]
        [SF] private List<ActionInfo> _onSuccess  = null;
        [SF] private List<ActionInfo> _onFailure  = null;
        [SF] private List<ActionInfo> _onChange   = null;
        [SF] private List<ActionInfo> _onComplete = null;

        private bool _isComplete = false;
        private int _index = 0;
        
// PROPERTIES

        private bool IsSequenceComplete => _index >= _sequence.Length;

// INITIALISATION

        /// <summary>
        /// Dummy so we can disable the script from inspector
        /// </summary>
        private void Start(){}

// SEQUENCE HANDLING

        public override void Enable()  => this.enabled = true;
        public override void Disable() => this.enabled = false;


        /// <summary>
        /// Updates the sequence
        /// </summary>
        public override void Compare(BaseInteraction pressed) {
            // Do nothing if the sequence is already complete
            if (!this.enabled || _isComplete) return;

            // Success and increment
            if (pressed.gameObject == _sequence[_index]){
                _index++;
                Interact(_onSuccess);
            
            } else { // Fail and reset
                Restore();
                Interact(_onFailure);
                return;
            }
            
            // Complete
            if (IsSequenceComplete){
                _isComplete = true;
                Interact(_onComplete);
            }

            Interact(_onChange, _index);
        }
        
        /// <summary>
        /// Resets the sequence
        /// </summary>
        public override void Restore() {
            _isComplete = false;
            _index = 0;

            Interact(_onChange, _index);
        }

// DATA HANDLING

        /// <summary>
        /// Returns the sequence action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onComplete,
                _onSuccess,
                _onFailure,
            };
        }

        /// <summary>
        /// Assigns the sequence actions
        /// </summary>
        public override void SetActions(List<List<ActionInfo>> actions){
            _onComplete = actions[0];
            _onSuccess  = actions[1];
            _onFailure  = actions[2];
        }
    }
}