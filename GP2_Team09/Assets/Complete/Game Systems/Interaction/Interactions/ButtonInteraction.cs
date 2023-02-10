using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Animator))]
    public class ButtonInteraction : BaseInteraction, IInteractable
    {
        [Header("Interactable")]
        [SF] private Sprite _hoverReticle = null;
        [SF] private Sprite _actionReticle = null;
        private Outline _outline;
        [Header("Methods")]
        [Space, SF] private List<ActionInfo> _onPressed  = new();
        [Space, SF] private List<ActionInfo> _onReleased = new();

        private bool _pressed = false;
        private Animator _animator = null;
        private readonly int PRESSED_HASH = Animator.StringToHash("Pressed");

// PROPERTIES

        public Sprite HoverReticle => _hoverReticle;
        public Sprite ActionReticle => _actionReticle;
        public Outline Outline => _outline;

// INITIALISATION

        /// <summary>
        /// Initialises the button
        /// </summary>
        private void Awake(){
            _animator = GetComponent<Animator>();
            _outline = GetComponent<Outline>();
        }

// BUTTON HANDLING

        /// <summary>
        /// Triggers button on player interaction
        /// </summary>
        public void Perform(bool interacting){
            _pressed = interacting;

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

        /// <summary>
        /// Assigns the button actions
        /// </summary>
        public override void SetActions(List<List<ActionInfo>> actions){
            _onPressed  = actions[0];
            _onReleased = actions[1];
        }
    }
}