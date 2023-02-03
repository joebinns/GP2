using SF = UnityEngine.SerializeField;
using UnityEngine.Events;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    public class TimerInteraction : MonoBehaviour
    {
        [SF] private float _threshold = 3f;
        [SF] private UpdateManager _update = null;
        [Space]
        [SF] private UnityEvent _onSuccess = new();
        [SF] private UnityEvent _onFailed  = new();
        [SF] private UnityEvent<float> _onChange = new();

        protected float _time = 0f;

// TIMER HANDLING

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void StartTimer(){
            _time = _threshold;
            ToggleTimer(true);
        }

        /// <summary>
        /// Stops the timer and invokes events based on result
        /// </summary>
        public void StopTimer(){
            ToggleTimer(false);

            if (_time > 0f)
                 _onFailed.Invoke();
            else _onSuccess.Invoke();
        }

        /// <summary>
        /// Increments the timer on update callback
        /// </summary>
        protected virtual void OnUpdateTimer(float deltaTime){
            _time -= deltaTime;
            _onChange.Invoke(_time);

            if (_time <= 0f)
                ToggleTimer(false);
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