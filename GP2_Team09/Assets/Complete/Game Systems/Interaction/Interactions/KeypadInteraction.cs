using SF = UnityEngine.SerializeField;
using UnityEngine;
using System.Collections.Generic;

namespace GameProject.Interactions {
    public class KeypadInteraction : BaseInteraction
    {
        [SF] private string _combination = null;
        
        [SF, Space] private List<ActionInfo> _onSuccess = null;
        [SF, Space] private List<ActionInfo> _onFailure = null;
        [SF, Space] private List<ActionInfo> _onChange  = null;
        [SF, Space] private List<ActionInfo> _onReset   = null;

        private string _input = string.Empty;

// KEYPAD HANDLING

        /// <summary>
        /// Assigns the value to the input
        /// </summary>
        public override void Changed(float value){
            if (_input.Length + 1 > _combination.Length) return;

            _input = string.Concat(_input, (int)value);
            Interact(_onChange, _input);
        }

        /// <summary>
        /// Checks if the input matches the combination
        /// </summary>
        public override void CheckResult(){
            var success = _input == _combination;

            if (success) Interact(_onSuccess);
            else Interact(_onFailure);

            Restore();
        }

        /// <summary>
        /// Removes the last entry
        /// </summary>
        public override void Remove(){
            if (_input.Length <= 0) return;
            
            _input = _input.Substring(
                0, _input.Length - 1
            );

            Interact(_onChange, _input);
        }

        /// <summary>
        /// Resets the current input
        /// </summary>
        public override void Restore(){
            _input = string.Empty;

            Interact(_onChange, _input);
        }
    }
}