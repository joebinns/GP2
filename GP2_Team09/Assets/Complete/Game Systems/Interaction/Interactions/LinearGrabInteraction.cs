using SF = UnityEngine.SerializeField;
using GameProject.Oscillators;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Oscillator))]
    public class LinearGrabInteraction : GrabInteraction
    {

// PROPERTIES

        public Oscillator Oscillator { get; private set; }

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