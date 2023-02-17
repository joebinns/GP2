using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using GameProject.Grab;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public abstract class GrabInteraction : BaseInteraction, IInteractable
    {
        [HideInInspector] public PlayerGrab PlayerGrab;
        
        [Header("Managers")]
        [SF] private UpdateManager _update = null;
        [Header("Methods")]
        [Space, SF] private List<ActionInfo> _onChange = null;
        [Header("Interaction")]
        [SF] private Transform _interactionPlane;

        protected InteractableType _interactableType;
        
        private Outline _outline;
        private bool _pressed;

// PROPERTIES

        public InteractableType InteractableType => _interactableType;
        public Outline Outline => _outline;
        public Transform InteractionPlane => _interactionPlane;
        public Rigidbody Rigidbody { get; private set; }

// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        protected virtual void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
            _outline = GetComponent<Outline>();
        }
        
// HOLD HANDLING

        /// <summary>
        /// Toggles hold on player interaction
        /// </summary>
        public void Perform(bool interacting){
            _pressed = interacting;

            if (_pressed) Grab();
            
            else Release();

            ToggleUpdate(_pressed);
        }

        /// <summary>
        /// Grabs this game object
        /// </summary>
        private void Grab() {
            PlayerGrab.Grab(this);
        }

        /// <summary>
        /// Releases this game object
        /// </summary>
        private void Release() {
            PlayerGrab.Release();
        }

        /// <summary>
        /// Calls on change on update callback
        /// </summary>
        private void OnUpdate(float deltaTime){
            Interact(_onChange);
        }

        /// <summary>
        /// Toggles the update loop
        /// </summary>
        private void ToggleUpdate(bool enabled){
            if (enabled) _update.Subscribe(OnUpdate, UpdateType.Update);
            else _update.Unsubscribe(OnUpdate, UpdateType.Update);
        }
    }
}