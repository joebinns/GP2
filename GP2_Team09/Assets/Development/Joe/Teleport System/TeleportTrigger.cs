using SF = UnityEngine.SerializeField;
using GameProject.Chaos;
using UnityEngine;

namespace GameProject
{
    public class TeleportTrigger : MonoBehaviour
    {
        [SF] private TeleportSystem _teleportSystem = null;
        [Space]
        [SF] private bool _isSafe = false;

        private void OnTriggerEnter(Collider other) {
            var rigidbody = other.GetComponent<Rigidbody>();
            if (!rigidbody) return;
            if (_isSafe) _teleportSystem.TryMarkSafe(rigidbody);
            else _teleportSystem.TryMarkUnsafe(rigidbody);
        }
    }
}
