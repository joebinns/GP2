using System.Collections;
using SF = UnityEngine.SerializeField;
using GameProject.Oscillators;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    [RequireComponent(typeof(Oscillator), typeof(TorsionalOscillator))]
    public class HoldInteraction : MonoBehaviour, IInteractable
    {
        private Transform _holdPivot;
        private Rigidbody _rigidbody;
        private Oscillator _oscillator;
        private TorsionalOscillator _torsionalOscillator;
        private bool _pressed;
        private const string HOLD_PIVOT = "Hold Pivot";

// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
            _oscillator = GetComponent<Oscillator>();
            _torsionalOscillator = GetComponent<TorsionalOscillator>();
        }

        /// <summary>
        /// TEMP: Set the hold pivot once the player has loaded
        /// </summary>
        private IEnumerator Start() { // TODO: Call this when the player is loaded
            yield return new WaitForSeconds(1f);
            _holdPivot = GameObject.FindWithTag(HOLD_PIVOT).transform;
        }

// HOLD HANDLING

        /// <summary>
        /// Toggles hold on player interaction
        /// </summary>
        public void Trigger(){
            _pressed = !_pressed;

            if (_pressed) 
                 Grab();
            
            else Release();
        }

        /// <summary>
        /// Grabs this game object
        /// </summary>
        private void Grab() {
            transform.SetParent(_holdPivot);
            _rigidbody.useGravity = false;
            _oscillator.enabled = true;
            _torsionalOscillator.enabled = true;
        }
        
        /// <summary>
        /// Releases this game object
        /// </summary>
        private void Release() {
            transform.SetParent(null);
            _rigidbody.useGravity = true;
            _oscillator.enabled = false;
            _torsionalOscillator.enabled = false;
        }
    }
}