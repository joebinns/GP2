using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Updates 
{
    [AddComponentMenu("Managers/Update Relay"), DefaultExecutionOrder(-1)]
    public sealed class UpdateRelay : MonoBehaviour
    {
        [SF] private bool _usePriority = false;
        [SF] private int  _startCapacity = 10;
        [SF] private UpdateManager _manager = null;
        private static UpdateRelay s_active = null;

// INITIALISATION

        /// <summary>
        /// Initialises the update manager
        /// </summary>
        private void Awake(){
            if (s_active == null){
                _manager.Initialise(
                    this, _startCapacity, _usePriority
                );
                s_active = this;

            } else Destroy(this);
        }

        /// <summary>
        /// Deinitialises the update manager
        /// </summary>
        public void OnDestroy(){
            if (s_active != this) return;
            _manager.OnDestroy();
        }

// UPDATE LOOPS

        /// <summary>
        /// Calls update on the update manager
        /// </summary>
        private void Update() 
            => _manager.OnUpdate(Time.deltaTime);

        /// <summary>
        /// Calls fixed update on the update manager
        /// </summary>
        private void FixedUpdate() 
            => _manager.OnFixedUpdate(Time.fixedDeltaTime);

        /// <summary>
        /// Calls late update on the update manager
        /// </summary>
        private void LateUpdate() 
            => _manager.OnLateUpdate(Time.deltaTime);
    }
}