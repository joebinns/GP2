using UnityEngine;

namespace GameProject.Oscillators
{
    /// <summary>
    /// Limits the range of position of this rigid body
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class LimitPosition : MonoBehaviour
    {
        [SerializeField] private Vector3 _minLocalPosition;
        [SerializeField] private Vector3 _maxLocalPosition;

        private Transform _transform;
        private Rigidbody _rigidbody;

        /// <summary>
        /// Define the rigid body
        /// </summary>
        private void Awake() {
            _transform = transform;
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        /// <summary>
        /// Clamp the position to be within the limits
        /// </summary>
        private void FixedUpdate() {
            // TODO: Make this work properly when Oscillator component is attached
            var clampedPosition = _transform.parent.TransformPoint(MathsUtilities.ClampVector3(_transform.localPosition, _minLocalPosition, _maxLocalPosition));
            _rigidbody.MovePosition(clampedPosition);
        }
    }
}

