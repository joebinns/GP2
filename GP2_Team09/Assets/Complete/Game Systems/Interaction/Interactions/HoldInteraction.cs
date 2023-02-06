using SF = UnityEngine.SerializeField;
using GameProject.Hold;
using GameProject.Oscillators;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    [RequireComponent(typeof(Oscillator), typeof(TorsionalOscillator))]
    public class HoldInteraction : MonoBehaviour, IInteractable
    {
        [HideInInspector] public PlayerHold PlayerHold;
        
        private bool _pressed;

        public Rigidbody Rigidbody { get; private set; }
        public Oscillator Oscillator { get; private set; }
        public TorsionalOscillator TorsionalOscillator { get; private set; }

// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        private void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
            Oscillator = GetComponent<Oscillator>();
            TorsionalOscillator = GetComponent<TorsionalOscillator>();
        }
        
// HOLD HANDLING

        /// <summary>
        /// Toggles hold on player interaction
        /// </summary>
        public void Trigger() {
            _pressed = !_pressed;

            if (_pressed) Grab();
            
            else Release();
        }

        /// <summary>
        /// Grabs this game object
        /// </summary>
        private void Grab() {
            PlayerHold.Grab(this);
        }

        /// <summary>
        /// Releases this game object
        /// </summary>
        private void Release() {
            PlayerHold.Release();
        }
    }
}