using SF = UnityEngine.SerializeField;
using System.Collections;
using UnityEngine;

namespace GameProject.Movement
{
    public class PlayerFall : MonoBehaviour
    {
        [SF] private CharacterController _characterController = null;
        [Space]
        [SF] private MovementSettings _settings = null;

        private Vector3 _gravity;
        private bool _isFalling;
        private PlayerCrouch _playerCrouch;
        private PlayerJump _playerJump;
        private float _radius;
        private Coroutine _fall;

        public float TimeSinceUngrounded { get; private set; }

        private const float GROUND_CHECK_DISTANCE_EPSILON = 0.1f;

// INITIALISATION

        /// <summary>
        /// Store frequently used values and references
        /// </summary>
        private void Awake() {
            _gravity = Physics.gravity;
            _playerCrouch = GetComponent<PlayerCrouch>();
            _playerJump = GetComponent<PlayerJump>();
            _radius = _characterController.radius;
        }

// MOVEMENT

        /// <summary>
        /// Update TimeSinceUngrounded, and if not already falling, start falling if ungrounded and not rising (jump) or crouching (crouch)
        /// </summary>
        private void Update() {
            if (IsGrounded) { TimeSinceUngrounded = 0f; return; }
            TimeSinceUngrounded += Time.deltaTime;
            if (_isFalling) return;
            if (_playerJump) if (_playerJump.State == PlayerJump.JumpState.Rising) return;
            if (_playerCrouch) if (_playerCrouch.State == PlayerCrouch.CrouchState.Crouching) return; // TODO: Hard to tell how much difference this is making, if any
            _fall = StartCoroutine(Fall());
        }
        
        /// <summary>
        /// Fall following the fall curve until grounded. If the fall curve is exhausted, then continue following it's final gradient
        /// </summary>
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
                if (IsGrounded) {
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
        /// Stop falling
        /// </summary>
        public void CancelFall() {
            if (!_isFalling) return;
            StopCoroutine(_fall);
            _isFalling = false;
        }

        /// <summary>
        /// Move character controller vertically, given a desired and previous vertical position
        /// </summary>
        private void SetVerticalPosition(float deltaVerticalPosition) => _characterController.Move(Vector3.up * (0.5f * deltaVerticalPosition));

        /// <summary>
        /// Check if the character controller is grounded
        /// </summary>
        private bool IsGrounded => Physics.SphereCast(transform.position + _characterController.center,
            _characterController.radius, _gravity.normalized, out var hit, 
            _characterController.height * 0.5f - _characterController.radius + GROUND_CHECK_DISTANCE_EPSILON);
        
        /// <summary>
        /// Draw a sphere illustrating the final condition of the sphere cast
        /// </summary>
        private void OnDrawGizmos() {
            if (!enabled) return;
            Gizmos.DrawWireSphere((transform.position + _characterController.center) + (_gravity.normalized * (_characterController.height * 0.5f - _radius + GROUND_CHECK_DISTANCE_EPSILON)), _radius);
        }
    }
}
