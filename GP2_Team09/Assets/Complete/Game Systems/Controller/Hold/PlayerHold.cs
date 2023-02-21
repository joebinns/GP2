using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;
using System;

namespace GameProject.Hold
{
    public class PlayerHold : MonoBehaviour
    {
        [SF] private InteractionSettings _settings = null;
        [Space]
        [SF] private Transform _holdPivot;
        
        private HoldInteraction _holding;
        
        public Transform HoldPivot => _holdPivot;
        private bool IsHolding => _holding != null;

// INITIALISATION
        
        /// <summary>
        /// Initialise the player hold and hold pivot references on all holdable game objects
        /// </summary>
        private void Start() {
            foreach (var interaction in FindObjectsOfType<HoldInteraction>()) {
                interaction.PlayerHold = this;
            }
        }

// HOLD HANDLING
        
        /// <summary>
        /// Grab the object to hold
        /// </summary>
        public void Grab(HoldInteraction toHold) {
            SetHold(toHold);
            
            // Set hold rotation
            if (_settings.AutoOrientOnGrab) _holdPivot.localRotation = Quaternion.identity;
            else _holdPivot.rotation = _holding.transform.rotation;
            
            OnGrab?.Invoke(_holding);
        }
        
        /// <summary>
        /// Update the oscillator and torsional oscillator equilibrium's, such as to copy the HoldPivot
        /// </summary>
        private void FixedUpdate() {
            if (!IsHolding) return;
            var holdingParent = _holding.transform.parent;
            _holding.Oscillator.LocalEquilibriumPosition = holdingParent.InverseTransformPoint(HoldPivot.position);
            _holding.TorsionalOscillator.LocalEquilibriumRotation = holdingParent.rotation * HoldPivot.rotation.eulerAngles;
        }

        /// <summary>
        /// Release any held object
        /// </summary>
        public void Release() {
            SetHold(null);
            OnRelease?.Invoke();
        }

        /// <summary>
        /// Sets the appropriate components controlling a game objects hold behaviour on or off as appropriate
        /// </summary>
        private void SetHold(HoldInteraction toHold) {
            var shouldHold = toHold != null;
            var holding = shouldHold ? toHold : _holding;
            
            if (_settings.ToggleGravity)
                holding.Rigidbody.useGravity = !shouldHold;
            holding.Oscillator.enabled = shouldHold;
            holding.TorsionalOscillator.enabled = shouldHold;

            _holding = toHold;
        }
        
// ACTIONS
        
        public event Action<HoldInteraction> OnGrab;
        public event Action OnRelease;
    }
}
