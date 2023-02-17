using SF = UnityEngine.SerializeField;
using System.Collections;
using System.Collections.Generic;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Chaos
{
    public class TeleportSystem : MonoBehaviour
    {
        [SF] private ChaosSettings _settings;
        [Space]
        [SF] private Transform _player;
        [SF] private Transform _teleportDestination;
        
        private Dictionary<Rigidbody, float> _unsafe = new Dictionary<Rigidbody, float>();
        private int _teleporting;
        private const float TELEPORT_BUFFER = 2f;

        public void TryMarkUnsafe(Rigidbody rigidbody) {
            if (!_unsafe.ContainsKey(rigidbody))
                _unsafe[rigidbody] = Time.time;
        }
        
        public void TryMarkSafe(Rigidbody rigidbody) {
            if (_unsafe.ContainsKey(rigidbody))
                _unsafe.Remove(rigidbody);
        }

        private void Update() {
            var time = Time.time;
            var toRemove = new List<Rigidbody>();
            foreach (var pair in _unsafe) {
                var duration = time - pair.Value;
                var distance = (pair.Key.transform.position - _player.position).magnitude;
                if (duration > _settings.MaxUnsafeDuration || distance > _settings.MaxUnsafeDistance) {
                    toRemove.Add(pair.Key);
                    StartCoroutine(Teleport(pair.Key, TELEPORT_BUFFER * _teleporting));
                }
            }
            foreach (var rigidbody in toRemove) {
                TryMarkSafe(rigidbody);
            }
        }

        private IEnumerator Teleport(Rigidbody rigidbody, float delay = 0f) {
            _teleporting++;
            yield return new WaitForSeconds(delay);
            var lookDirection = _teleportDestination.position - _player.position;
            lookDirection.y = 0f;
            yield return StartCoroutine(UnityUtilities.ResetRigidbody(rigidbody, _teleportDestination.position, Quaternion.LookRotation(lookDirection)));
            rigidbody.velocity = Vector3.down;
            _teleporting--;
        }
        
    }
}
