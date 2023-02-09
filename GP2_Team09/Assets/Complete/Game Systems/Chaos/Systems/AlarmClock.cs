using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using GameProject.HUD;
using UnityEngine;

namespace GameProject.Chaos
{
    [RequireComponent(typeof(Rigidbody))]
    public class AlarmClock : BaseInteraction {
        [SF] private ChaosSettings _settings;
        [SF] private UITimer _uiTimer;
        [SF] private AudioSource _audioSource;

        private bool _isPlaying;
        private Rigidbody _rigidbody;
        private Vector3 _previousVelocity;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public override void Trigger() {
            _audioSource.Play();
            _isPlaying = true;
        }

        private void FixedUpdate() {
            if (!_isPlaying) return;
            
            if (CalculateDeceleration().magnitude > _settings.DecelerationThreshold) {
                Reset();
            }
        }

        private Vector3 CalculateDeceleration() {
            var velocity = _rigidbody.velocity;
            var deceleration = (velocity - _previousVelocity) * Time.fixedDeltaTime;
            var isDecelerating = Vector3.Dot(velocity, -_previousVelocity) < 0;
            _previousVelocity = velocity;
            return isDecelerating ? deceleration : Vector3.zero;
        }

        private void Reset() {
            _audioSource.Stop();
            _uiTimer.Restore();
            _uiTimer.OverrideThreshold(Random.Range(_settings.RandomTimerRange.x, _settings.RandomTimerRange.y));
            _uiTimer.Begin();
            _isPlaying = false;
        }
    }
}
