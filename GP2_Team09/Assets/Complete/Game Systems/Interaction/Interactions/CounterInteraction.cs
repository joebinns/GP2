using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    public class CounterInteraction : BaseInteraction
    {
        [SF] private int _start = 0;
        [SF] private int _threshold = 3;
        [Space]
        [Space, SF] private List<ActionInfo> _onSuccess   = null;
        [Space, SF] private List<ActionInfo> _onFailure   = null;
        [Space, SF] private List<ActionInfo> _onIncrement = null;
        [Space, SF] private List<ActionInfo> _onDecrement = null;
        [Space, SF] private List<ActionInfo> _onReset     = null;

        private int _count = 0;

// INITIALISATION

        private void Start() => _count = _start;

// COUNTER HANDLING

        /// <summary>
        /// Increments the counter and invokes events based on count
        /// </summary>
        public override void Increment(){
            Interact(_onIncrement, ++_count);
        }

        /// <summary>
        /// Decrements the counter and invokes events based on count
        /// </summary>
        public override void Decrement(){
            Interact(_onDecrement, --_count);
        }

        /// <summary>
        /// Checks if the count matches the threshold
        /// </summary>
        public override void Finalise(){
            if (_count == _threshold)
                Interact(_onSuccess);

            else Interact(_onFailure);
        }

        /// <summary>
        /// Resets the counter
        /// </summary>
        public override void Restore(){
            Start();

            Interact(_onReset, _count);
        }

// DATA HANDLING

        /// <summary>
        /// Returns the counter action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onSuccess,
                _onFailure,
                _onIncrement,
                _onDecrement,
                _onReset
            };
        }

        /// <summary>
        /// Assigns the counter actions
        /// </summary>
        public override void SetActions(List<List<ActionInfo>> actions){
            _onSuccess   = actions[0];
            _onFailure   = actions[1];
            _onIncrement = actions[2];
            _onDecrement = actions[3];
            _onReset     = actions[4];
        }
    }
}