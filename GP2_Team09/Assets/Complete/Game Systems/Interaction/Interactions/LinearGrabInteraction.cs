using SF = UnityEngine.SerializeField;
using GameProject.Oscillators;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Oscillator))]
    public class LinearGrabInteraction : GrabInteraction
    {
        [Header("Interaction")]
        [SF] private Transform _interactionPlane;
        [SF] private Transform _altInteractionPlane;

// PROPERTIES

        public Oscillator Oscillator { get; private set; }
        public Transform InteractionPlane => _interactionPlane;
        public Transform AltInteractionPlane => _altInteractionPlane;

// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        protected override void Awake() {
            base.Awake();
            _interactableType = InteractableType.LinearGrab;
            Oscillator = GetComponent<Oscillator>();
        }
    }
}