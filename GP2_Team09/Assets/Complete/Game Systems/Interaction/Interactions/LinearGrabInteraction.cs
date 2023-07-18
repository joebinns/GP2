using GameProject.Oscillators;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Oscillator))]
    public class LinearGrabInteraction : GrabInteraction
    {
        [Header("Movement")]
        [SerializeField] private Transform _movementAxis;

// PROPERTIES

        public Oscillator Oscillator { get; private set; }
        public Transform MovementAxis => _movementAxis;

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