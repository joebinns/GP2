using System;
using SF = UnityEngine.SerializeField;
using GameProject.Oscillators;
using UnityEngine;

namespace GameProject.Interactions
{
    [RequireComponent(typeof(TorsionalOscillator))]
    public class TorsionalGrabInteraction : GrabInteraction
    {
        [Header("Rotation")]
        [SF] private Transform _rotationAxis;
        
        [Header("Limits")]
        [SF] private bool _useLimits = false;
        [SF] private float _minAngle = -180f;
        [SF] private float _maxAngle = 180f;
        
        private Vector3 _localTargetRotation = Vector3.zero;

// PROPERTIES

        public TorsionalOscillator TorsionalOscillator { get; private set; }
        public Transform RotationAxis => _rotationAxis;
        public float Angle => Vector3.Dot(_localTargetRotation, LocalRotationAxis);
        public float MinAngle => _minAngle;
        public float MaxAngle => _maxAngle;
        private Vector3 LocalRotationAxis => TorsionalOscillator.transform.InverseTransformDirection(_rotationAxis.forward);
        private Vector3 LocalEulerAngles => TorsionalOscillator.transform.localEulerAngles;
        
// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        protected override void Awake() {
            base.Awake();
            _interactableType = InteractableType.TorsionalGrab;
            TorsionalOscillator = GetComponent<TorsionalOscillator>();
            _localTargetRotation = TorsionalOscillator.LocalEquilibriumRotation;
        }

        /// <summary>
        /// Dummy Start method to allow enabling and disabling component from inspector
        /// </summary>
        private void Start() {
        }

        // ROTATIONS

        public void AdjustEquilibrium(float deltaAngle) {
            _localTargetRotation += deltaAngle * LocalRotationAxis;
            _localTargetRotation = _useLimits ? Mathf.Clamp(Angle, _minAngle, _maxAngle) * LocalRotationAxis : _localTargetRotation;
            TorsionalOscillator.LocalEquilibriumRotation = _localTargetRotation;
        }
    }
}