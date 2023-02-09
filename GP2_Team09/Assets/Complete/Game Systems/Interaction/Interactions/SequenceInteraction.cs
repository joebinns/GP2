using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    public class SequenceInteraction : BaseInteraction
    {
        [SF] private GameObject[] _sequence = null;
        [Space]
        [SF] private List<ActionInfo> _onComplete = null;
        [SF] private List<ActionInfo> _onSuccess = null;
        [SF] private List<ActionInfo> _onFailure = null;

        private bool _isComplete = false;
        private int _index = 0;
        
// SEQUENCE HANDLING

        public override void Compare(BaseInteraction pressed) {
            if (_isComplete) return; // Do nothing if the sequence is already complete

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
        }

        private bool IsSequenceComplete => _index >= _sequence.Length;

        public override void Restore() {
            _isComplete = false;
            _index = 0;
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