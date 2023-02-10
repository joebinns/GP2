using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using GameProject.Oscillators;
using GameProject.Grab;
using UnityEngine;
using GameProject.Updates;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(TorsionalOscillator))]
    public class GrabInteraction : BaseInteraction, IInteractable
    {
        [HideInInspector] public PlayerGrab PlayerGrab;
        
        [SF] private Sprite _hoverReticle = null;
        [SF] private Sprite _actionReticle = null;
        [SF] private UpdateManager _update = null;
        [Space, SF] private List<ActionInfo> _onChange = null;

        private bool _pressed;

// PROPERTIES

        public Sprite HoverReticle => _hoverReticle;
        public Sprite ActionReticle => _actionReticle;
        public Rigidbody Rigidbody { get; private set; }
        public TorsionalOscillator TorsionalOscillator { get; private set; }
        
// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        private void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
            TorsionalOscillator = GetComponent<TorsionalOscillator>();
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