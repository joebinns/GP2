using UnityEngine;

namespace GameProject.Oscillators
{
    /// <summary>
    /// Limits the range of rotation of this rigid body
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class LimitRotation : MonoBehaviour
    {
        // +- Range of rotations for each respective axis
        [SerializeField] private Vector3 maxLocalRotation = Vector3.one * 360f;

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
        /// Clamp the rotation to be less than the desired maxLocalRotation
        /// </summary>
        private void FixedUpdate() {
            var clampedRotation = _transform.parent.rotation * MathsUtilities.ClampRotation(_transform.localRotation, maxLocalRotation);
            _rigidbody.MoveRotation(clampedRotation);
        }
    }
}

