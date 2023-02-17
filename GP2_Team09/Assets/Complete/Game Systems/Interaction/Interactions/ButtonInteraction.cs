using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Interactions
{
    public class ButtonInteraction : BaseInteraction, IInteractable
    {
        private InteractableType _interactableType;
        private Outline _outline;
        
        [Header("Interaction")]
        [SF] private Animator _animator = null;
        
        [Header("Methods")]
        [Space, SF] protected List<ActionInfo> _onPressed  = new();
        [Space, SF] protected List<ActionInfo> _onReleased = new();
        
        protected bool _pressed = false;
        
        private readonly int PRESSED_HASH = Animator.StringToHash("Pressed");

// PROPERTIES

        public InteractableType InteractableType => _interactableType;
        public Outline Outline => _outline;

// INITIALISATION

        /// <summary>
        /// Initialises the button
        /// </summary>
        protected virtual void Awake(){
            _interactableType = InteractableType.Press;
            _animator ??= GetComponent<Animator>();
            _outline = GetComponent<Outline>();
        }

        /// <summary>
        /// Dummy so the script can be disabled
        /// </summary>
        protected virtual void Start(){}

// BUTTON HANDLING

        public override void Enable()  => this.enabled = true;
        public override void Disable() => this.enabled = false;


        /// <summary>
        /// Triggers button on player interaction
        /// </summary>
        public virtual void Perform(bool interacting){
            if (!this.enabled) return;

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