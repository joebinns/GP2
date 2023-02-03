using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Game
{
    [AddComponentMenu("Managers/Game Initialiser"),
     DefaultExecutionOrder(-1), DisallowMultipleComponent]
    public sealed class GameInitialiser : MonoBehaviour
    {
        [SF] private GameManager _manager = null;

        private static GameInitialiser s_active = null;

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