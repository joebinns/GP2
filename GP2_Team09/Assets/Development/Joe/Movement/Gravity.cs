using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Movement
{
    public class Gravity : MonoBehaviour
    {
        [SF] private CharacterController _characterController = null;

        private float _fallSpeed = 0f;
        private Vector3 _gravity;
        
// INITIALISATION
        
        /// <summary>
        /// Fetch values from frequently used references
        /// </summary>
        private void Awake() {
            _gravity = Physics.gravity;
        }

// MOVEMENT
        
        /// <summary>
        /// Apply gravity based on if the character controller is grounded
        /// </summary>
        private void Update() {
            if (IsGrounded) ResetGravity();
            else ApplyGravity();
        }

        /// <summary>
        /// Check if the character controller is grounded
        /// </summary>
        private bool IsGrounded => Physics.SphereCast(transform.position + _characterController.center,
            _characterController.radius, _gravity.normalized, out var hit, 
            (_characterController.height * 0.51f - _characterController.radius * 0.5f));

        /// <summary>
        /// Accelerate speed and apply movement
        /// </summary>
        private void ApplyGravity() {
            _fallSpeed += _gravity.y * Time.deltaTime;
            _characterController.Move(Vector3.up * (_fallSpeed * Time.deltaTime));
        }

        /// <summary>
        /// Reset the fall speed
        /// </summary>
        private void ResetGravity() => _fallSpeed = 0f;
    }
}
