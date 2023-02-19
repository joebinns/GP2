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


        private Vector3 _initialRotation = Vector3.zero;
        private Vector3 _rotation = Vector3.zero;

        public Transform RotationAxis => _rotationAxis;
        public bool UseLimits => _useLimits;
        public float MinAngle => _minAngle;
        public float MaxAngle => _maxAngle;
        
// PROPERTIES

        public TorsionalOscillator TorsionalOscillator { get; private set; }
        
// INITIALISATION

        /// <summary>
        /// Sets frequently used references
        /// </summary>
        protected override void Awake() {
            base.Awake();
            _interactableType = InteractableType.TorsionalGrab;
            TorsionalOscillator = GetComponent<TorsionalOscillator>();
            _initialRotation = TorsionalOscillator.LocalEquilibriumRotation;
            _rotation = _initialRotation;
        }
        
// TRACKING ROTATIONS

        public void AdjustEquilibrium(Vector3 deltaAngle) {
            _rotation += deltaAngle;
            TorsionalOscillator.LocalEquilibriumRotation = _rotation;

            //Debug.Log(_rotation);

            //Vector3.SignedAngle()
        }
    }
}