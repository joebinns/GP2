using SF = UnityEngine.SerializeField;
using GameProject.Hold;
using GameProject.Oscillators;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    [RequireComponent(typeof(Oscillator), typeof(TorsionalOscillator))]
    public class HoldInteraction : BaseInteraction, IInteractable
    {
        [HideInInspector] public PlayerHold PlayerHold;
        
        [Header("Interactable")]
        [SF] private Sprite _hoverReticle = null;
        [SF] private Sprite _actionReticle = null;
        private Outline _outline;
        
        private bool _pressed = false;

// PROPERTIES

        public Sprite HoverReticle => _hoverReticle;
        public Sprite ActionReticle => _actionReticle;
        public Outline Outline => _outline;
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