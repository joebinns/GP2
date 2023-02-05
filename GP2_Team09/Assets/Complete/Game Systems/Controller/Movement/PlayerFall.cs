using System;
using SF = UnityEngine.SerializeField;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace GameProject.Movement
{
    public class PlayerFall : MonoBehaviour
    {
        [SF] private CharacterController _characterController = null;
        [Space]
        [SF] private MovementSettings _settings = null;

        private Vector3 _gravity;

// INITIALISATION

        /// <summary>
        /// Fetch values from frequently used references
        /// </summary>
        private void Awake() {
            _gravity = Physics.gravity;
            
        }

        private void Start() {
            _previousPositionY = _characterController.transform.position.y;
        }

        // MOVEMENT

        /// <summary>
        /// Check if the character controller is grounded
        /// </summary>
        private bool IsGrounded => Physics.SphereCast(transform.position + _characterController.center,
            _characterController.radius, _gravity.normalized, out var hit, 
            (_characterController.height * 0.51f - _characterController.radius * 0.5f));
        
        //private bool IsRising => _characterController.velocity.y > 0; // Character controller velocity not updating properly
        private float _previousPositionY;
        private bool IsRising() {
            var positionY = _characterController.transform.position.y;
            var velocityY = positionY - _previousPositionY;
            _previousPositionY = positionY;
            return velocityY > 0;
        }
        
        private bool _isFalling;
        
        private void Update() {
            var isRising = IsRising();
            if (!IsGrounded && !_isFalling && !isRising) {
                StartCoroutine(Fall());
            }
        }
        
        private IEnumerator Fall() {
            _isFalling = true;
            
            var t = 0f;
            var curve = _settings.FallCurve;
            var previousValue = curve.Evaluate(t);
            var finalFrame = curve[curve.length - 1];
            while (t < finalFrame.time) {
                t += Time.deltaTime;
                var value = curve.Evaluate(t);
                SetVerticalPosition(value - previousValue);
                previousValue = value;
                if (IsGrounded) { // TODO: Prevent this if just recently starting jumping
                    t = finalFrame.time;
                }
                yield return new WaitForEndOfFrame();
            }
            
            // Continue falling along the same trajectory, until _grounded
            var velocityY = curve.Differentiate(finalFrame.time);
            while (!IsGrounded) {
                SetVerticalPosition(velocityY * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            _isFalling = false;
        }

        /// <summary>
        /// Move character controller vertically, given a desired and previous vertical position
        /// </summary>
        private void SetVerticalPosition(float deltaVerticalPosition) => _characterController.Move(Vector3.up * (0.5f * deltaVerticalPosition));
    }
}
