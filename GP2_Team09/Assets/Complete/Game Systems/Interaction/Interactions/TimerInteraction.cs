using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    public class TimerInteraction : BaseInteraction
    {
        [SF] private float _threshold = 3f;
        [SF] private UpdateManager _update = null;

        [Space, SF] private List<ActionInfo> _onSuccess = null;
        [Space, SF] private List<ActionInfo> _onFailure  = null;
        [Space, SF] private List<ActionInfo> _onChange   = null;

        protected float _time = 0f;

// TIMER HANDLING

        /// <summary>
        /// Sets the threshold
        /// </summary>
        protected virtual void SetThreshold(float threshold) {
            _threshold = threshold;
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public override void Begin(){
            _time = _threshold;
            ToggleTimer(true);
        }

        /// <summary>
        /// Stops the timer and invokes events based on result
        /// </summary>
        public override void End(){
            ToggleTimer(false);
            
            if (_time <= 0f) Interact(_onFailure);
            else Interact(_onSuccess);
        }

        /// <summary>
        /// Resets the time
        /// </summary>
        public override void Restore(){
            _time = _threshold;
        }

        /// <summary>
        /// Increments the timer on update callback
        /// </summary>
        protected virtual void OnUpdateTimer(float deltaTime){
            _time -= deltaTime;

            if (_time <= 0f) {
                End();
            }
            
            Interact(_onChange, _time);
        }

        /// <summary>
        /// Toggles the update loop
        /// </summary>
        private void ToggleTimer(bool enabled){
            if (enabled) _update.Subscribe(OnUpdateTimer, UpdateType.Update);
            else _update.Unsubscribe(OnUpdateTimer, UpdateType.Update);
        }

// DATA HANDLING

        /// <summary>
        /// Returns the timer action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onSuccess,
                _onFailure,
                _onChange,
            };
        }

        /// <summary>
        /// Assigns the sequence actions
        /// </summary>
        public override void SetActions(List<List<ActionInfo>> actions){
            _onSuccess = actions[0];
            _onFailure = actions[1];
            _onChange  = actions[2];
        }
    }
}