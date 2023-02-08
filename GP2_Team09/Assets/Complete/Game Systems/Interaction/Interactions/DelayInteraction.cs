using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using GameProject.Updates;
using UnityEngine;

namespace GameProject.Interactions
{
    public class DelayInteraction : BaseInteraction
    {
        [SF] private float delay = 0f;
        [SF] private UpdateManager _update = null;

        [Space, SF] private List<ActionInfo> _onComplete = null;

        protected float _time = 0f;

// DELAY HANDLING

        /// <summary>
        /// Starts the timer
        /// </summary>
        public override void Trigger() => Begin();

        /// <summary>
        /// Starts the timer
        /// </summary>
        public override void Begin(){
            _time = delay;
            ToggleTimer(true);
        }


        /// <summary>
        /// Increments the timer on update callback
        /// </summary>
        protected virtual void OnUpdateTimer(float deltaTime){
            _time -= deltaTime;
            if (_time > 0f) return;

            ToggleTimer(false);
            Interact(_onComplete);
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
        /// Returns the delay action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onComplete
            };
        }
    }
}