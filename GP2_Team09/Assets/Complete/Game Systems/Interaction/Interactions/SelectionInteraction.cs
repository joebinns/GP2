using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    public class SelectionInteraction : BaseInteraction
    {
        [SS] public class SelectionInfo {
            public string Key = string.Empty;
            public List<ActionInfo> OnSelected = null;
        }

        [SF] private int _maxCharacters = 10;
        [Space, SF] private List<SelectionInfo> _selections = null;
        [Space, SF] private List<ActionInfo> _onChange = null;

        private string _input = string.Empty;

// SELECTION HANDLING

        /// <summary>
        /// Assigns the value to the input
        /// </summary>
        public override void Changed(float value){
            if (_input.Length + 1 > _maxCharacters) return;

            _input = string.Concat(_input, (int)value);
            Interact(_onChange, _input);
        }

        /// <summary>
        /// Checks and triggers the matching selection
        /// </summary>
        public override void CheckResult(){
            for (int i = 0; i < _selections.Count; i++){
                var info = _selections[i];
                
                if (info.Key != _input) continue;
                Interact(info.OnSelected);
            }

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