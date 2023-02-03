using SF = UnityEngine.SerializeField;
using UnityEngine.Events;
using UnityEngine;

namespace GameProject.Interactions
{
    public class CounterInteraction : MonoBehaviour
    {
        [SF] private int _start = 0;
        [SF] private int _threshold = 3;
        [Space]
        [SF] private UnityEvent _onSuccess = new();
        [SF] private UnityEvent<int> _onIncrement = new();
        [SF] private UnityEvent<int> _onDecrement = new();
        [SF] private UnityEvent<int> _onReset     = new();

        private int _count = 0;

// INITIALISATION

        private void Start() => _count = _start;

// COUNTER HANDLING

        /// <summary>
        /// Increments the counter and invokes events based on count
        /// </summary>
        public void Increment(){
            if (++_count >= _threshold)
                _onSuccess.Invoke();

            _onIncrement.Invoke(_count);
        }

        /// <summary>
        /// Decrements the counter and invokes events based on count
        /// </summary>
        public void Decrement(){
            if (--_count <= _threshold)
                _onSuccess.Invoke();

            _onDecrement.Invoke(_count);
        }

        /// <summary>
        /// Resets the counter
        /// </summary>
        public void Reset(){
            Start();

            _onReset.Invoke(_count);
        }
    }
}