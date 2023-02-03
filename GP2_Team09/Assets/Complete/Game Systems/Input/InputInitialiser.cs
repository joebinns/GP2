using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Inputs
{
    [AddComponentMenu("Managers/Input Initialiser"),
     DefaultExecutionOrder(-1), DisallowMultipleComponent]
    public sealed class InputInitialiser : MonoBehaviour
    {
        [SF] private InputManager _manager = null;

        private static InputInitialiser s_active = null;

// INITIALISATION

        /// <summary>
        /// Initialises the input manager
        /// </summary>
        private void Awake(){
            if (s_active == null){
                _manager.Initialise();
                s_active = this;

            } else Destroy(this);
        }

        /// <summary>
        /// Deinitialises the input manager
        /// </summary>
        private void OnDestroy(){
            if (s_active != this) return;
            _manager.OnDestroy();
        }
    }
}
