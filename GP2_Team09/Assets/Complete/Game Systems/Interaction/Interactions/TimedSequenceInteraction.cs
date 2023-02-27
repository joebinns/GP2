using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Updates;
using System.Collections.Generic;

namespace GameProject.Interactions
{
    public class TimedSequenceInteraction : SequenceInteraction
    {
        [Header("Timer Settings")]
        [SF] private float _threshold = 3f;
        [SF] private UpdateManager _update = null;
        [Space, SF] private List<ActionInfo> _onTimedOut = null;
        [Space, SF] private List<ActionInfo> _onTick     = null;

        protected float _time = 0f;

// SEQUENCE HANDLING

        /// <summary>
        /// Updates the sequence and starts the timer
        /// </summary>
        public override void Compare(BaseInteraction pressed){
            if (!this.enabled || _isComplete) return;
            base.Compare(pressed);

            if (_isComplete){
                ToggleTimer(false);
                return;

            } else if (_index == 1){
                ToggleTimer(true);
                Begin();
            }
        }

        /// <summary>
        /// Resets the sequence and time
        /// </summary>
        public override void Restore(){
            base.Restore();
            ToggleTimer(false);
            Begin();
        }

        /// <summary>
        /// Begins the timer
        /// </summary>
        public override void Begin(){
            _time = _threshold;
        }

        /// <summary>
        /// Increments the timer on update callback
        /// </summary>
        protected virtual void OnUpdateTimer(float deltaTime){
            _time -= deltaTime;
            Interact(_onTick, _time);

            if (_time > 0f) return;
            Interact(_onTimedOut);
            Restore();
        }

        /// <summary>
        /// Toggles the update loop
        /// </summary>
        private void ToggleTimer(bool enabled){
            if (enabled) _update.Subscribe(OnUpdateTimer, UpdateType.Update);
            else _update.Unsubscribe(OnUpdateTimer, UpdateType.Update);
        }
    }
}