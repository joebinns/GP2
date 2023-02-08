using SF = UnityEngine.SerializeField;
using GameProject.Oscillators;
using GameProject.Grab;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(TorsionalOscillator))]
    public class GrabInteraction : MonoBehaviour, IInteractable
    {
        [HideInInspector] public PlayerGrab PlayerGrab;
        
        [SF] private Sprite _hoverReticle = null;
        [SF] private Sprite _actionReticle = null;
        
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
        public void Perform() {
            _pressed = !_pressed;

            if (_pressed) Grab();
            
            else Release();
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
    }
}