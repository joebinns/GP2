using SF = UnityEngine.SerializeField;
using UnityEngine.Events;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Animator))]
    public class ButtonInteraction : MonoBehaviour, IInteractable
    {
        [SF] private UnityEvent _onPressed  = new();
        [SF] private UnityEvent _onReleased = new();

        private bool _pressed = false;

        private Animator _animator = null;
        private readonly int PRESSED_HASH = 
            Animator.StringToHash("Pressed");

// INITIALISATION

        /// <summary>
        /// Initialises the button
        /// </summary>
        private void Awake(){
            _animator = GetComponent<Animator>();
        }

// BUTTON HANDLING

        /// <summary>
        /// Triggers button on player interaction
        /// </summary>
        public void Trigger(){
            _pressed = !_pressed;
            _animator.SetBool(PRESSED_HASH, _pressed);

            if (_pressed) _onPressed.Invoke();
            else _onReleased.Invoke();
        }
    }
}