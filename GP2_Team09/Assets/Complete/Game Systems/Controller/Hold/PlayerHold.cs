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
        private bool _isHolding;
        
        public Transform HoldPivot => _holdPivot;

        /// <summary>
        /// Initialise the player hold and hold pivot references on all holdable game objects
        /// </summary>
        private void Start() {
            foreach (var interaction in FindObjectsOfType<HoldInteraction>()) {
                interaction.PlayerHold = this;
            }
        }

        public void Grab(HoldInteraction toHold) {
            _holding = toHold;
            _isHolding = true;
            _holding.Rigidbody.useGravity = false;
            _holding.Oscillator.enabled = true;
            _holding.TorsionalOscillator.enabled = true;
            
            // Set hold rotation
            if (!_settings.AutoOrientOnGrab) _holdPivot.rotation = _holding.transform.rotation;
            else _holdPivot.localRotation = Quaternion.identity;
            
            OnGrab?.Invoke(_holding);
        }
        
        private void FixedUpdate()
        {
            if (!_isHolding) return;
            _holding.Oscillator.LocalEquilibriumPosition = HoldPivot.position;
            _holding.TorsionalOscillator.LocalEquilibriumRotation = HoldPivot.rotation.eulerAngles;
        }

        public void Release() {
            _holding.Rigidbody.useGravity = true;
            _holding.Oscillator.enabled = false;
            _holding.TorsionalOscillator.enabled = false;
            _isHolding = false;
            _holding = null;
            OnRelease?.Invoke();
        }
        
        public event Action<HoldInteraction> OnGrab;
        public event Action OnRelease;
    }
}
