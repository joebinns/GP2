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

        private Vector3 _localRotation = Vector3.zero;
        private Vector3 _localTargetRotation = Vector3.zero;

// PROPERTIES

        public TorsionalOscillator TorsionalOscillator { get; private set; }
        public Transform RotationAxis => _rotationAxis;
        public float Angle => Vector3.Dot(_localRotation, LocalRotationAxis);
        public float TargetAngle => Vector3.Dot(_localTargetRotation, LocalRotationAxis);
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
            _localRotation = LocalEulerAngles;
            _localTargetRotation = TorsionalOscillator.LocalEquilibriumRotation;
            _angle = LocalEulerAngles;
        }
        
// ROTATIONS

        public void AdjustTargetRotation(float deltaAngle) {
            _localTargetRotation += deltaAngle * LocalRotationAxis;
            _localTargetRotation = _useLimits ? Mathf.Clamp(TargetAngle, _minAngle, _maxAngle) * LocalRotationAxis : _localTargetRotation;
            TorsionalOscillator.LocalEquilibriumRotation = _localTargetRotation;
        }

        private void Update()
        {
            TrackEulerAngles();
        }

        private void FixedUpdate() {
            var parent = transform.parent;
            //TrackEulerAngles();
            
            /*
            _localRotation = _useLimits ? Mathf.Clamp(Angle, _minAngle, _maxAngle) * LocalRotationAxis : _localRotation;
            var rotation = Quaternion.Euler(_localRotation);
            if (parent != null) // Convert local rotation to world space
                rotation = parent.rotation * rotation;
            Rigidbody.rotation = rotation;

            Debug.Log(_localRotation);
            Debug.Log(Mathf.Clamp(Angle, _minAngle, _maxAngle) * LocalRotationAxis);
            Debug.Log(Angle);
            */

            if (_useLimits) {
                if (Angle < _minAngle || Angle > _maxAngle) {
                    _localRotation = Mathf.Clamp(Angle, _minAngle, _maxAngle) * LocalRotationAxis;
                    Rigidbody.rotation = parent.rotation * Quaternion.Euler(_localRotation);
                }
            }
            
        }

        private Vector3 _angle;
        
        private void TrackEulerAngles() {
            var angle = LocalEulerAngles;
            var deltaAngle = new Vector3(MathsUtilities.GetDeltaAngle(ref angle.x, _angle.x), 
                MathsUtilities.GetDeltaAngle(ref angle.y, _angle.y),
                MathsUtilities.GetDeltaAngle(ref angle.z, _angle.z));
            _angle = angle;
            _localRotation += deltaAngle;
        }
        
        // TODO: Set TorsionalOscillator.LocalEquilibriumRotation = transform.localEulerAngles on Grab() -- HMM BUT I NEED TO BE ABLE TO BE > 360f...
    }
}