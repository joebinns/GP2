using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Animator))]
    public class ButtonInteraction : BaseInteraction, IInteractable
    {
        [SF] private Sprite _hoverReticle = null;
        [SF] private Sprite _actionReticle = null;
        [Space, SF] private List<ActionInfo> _onPressed  = new();
        [Space, SF] private List<ActionInfo> _onReleased = new();

        private bool _pressed = false;
        private Animator _animator = null;
        private readonly int PRESSED_HASH = Animator.StringToHash("Pressed");

// PROPERTIES

        public Sprite HoverReticle => _hoverReticle;
        public Sprite ActionReticle => _actionReticle;

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
        public void Perform(){
            _pressed = !_pressed;
            _animator.SetBool(PRESSED_HASH, _pressed);

            if (_pressed) Interact(_onPressed);
            else Interact(_onReleased);
        }

// DATA HANDLING

        /// <summary>
        /// Returns the button action lists
        /// </summary>
        public override List<List<ActionInfo>> GetActions(){
            return new List<List<ActionInfo>>(){
                _onPressed, 
                _onReleased
            };
        }
    }
}