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
        
        private Vector3 _rotation = Vector3.zero;

// PROPERTIES

        public TorsionalOscillator TorsionalOscillator { get; private set; }
        public Transform RotationAxis => _rotationAxis;
        public float Angle => Vector3.Dot(_rotation.normalized, _rotation.magnitude * Vector3.one);
        public float MinAngle => _minAngle;
        public float MaxAngle => _maxAngle;
        
// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        protected override void Awake() {
            base.Awake();
            _interactableType = InteractableType.TorsionalGrab;
            TorsionalOscillator = GetComponent<TorsionalOscillator>();
            _rotation = TorsionalOscillator.LocalEquilibriumRotation;
        }
        
// ROTATIONS

        public void AdjustEquilibrium(Vector3 deltaAngle) {
            _rotation += deltaAngle;
            _rotation = _useLimits ? Mathf.Abs(Mathf.Clamp(Angle, _minAngle, _maxAngle)) * _rotation.normalized : _rotation;
            TorsionalOscillator.LocalEquilibriumRotation = _rotation;
        }
    }
}