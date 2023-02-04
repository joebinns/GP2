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
        [HideInInspector] public Transform HoldPivot;
        
        private Rigidbody _rigidbody;
        private Oscillator _oscillator;
        private TorsionalOscillator _torsionalOscillator;
        private bool _pressed;
        private bool _isHeld;

// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _oscillator = GetComponent<Oscillator>();
            _torsionalOscillator = GetComponent<TorsionalOscillator>();
        }
        
// HOLD HANDLING

        /// <summary>
        /// Toggles hold on player interaction
        /// </summary>
        public void Trigger() {
            if (!HoldPivot) return;
            
            _pressed = !_pressed;

            if (_pressed) 
                 Grab();
            
            else Release();
        }

        /// <summary>
        /// Grabs this game object
        /// </summary>
        private void Grab() {
            _rigidbody.useGravity = false;
            _oscillator.enabled = true;
            _torsionalOscillator.enabled = true;
            _isHeld = true;
            PlayerHold.IsHolding = true;
        }

        private void FixedUpdate()
        {
            if (!_isHeld) return;
            _oscillator.LocalEquilibriumPosition = HoldPivot.position;
            _torsionalOscillator.LocalEquilibriumRotation = HoldPivot.rotation.eulerAngles;
        }

        /// <summary>
        /// Releases this game object
        /// </summary>
        private void Release() {
            _rigidbody.useGravity = true;
            _oscillator.enabled = false;
            _torsionalOscillator.enabled = false;
            _isHeld = false;
            PlayerHold.IsHolding = false;
        }
    }
}