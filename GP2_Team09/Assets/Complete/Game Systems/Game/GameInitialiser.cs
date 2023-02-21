using SF = UnityEngine.SerializeField;
using UnityEngine.UI;
using UnityEngine;

namespace GameProject.Game
{
    [AddComponentMenu("Managers/Game Initialiser"),
     DefaultExecutionOrder(-1), DisallowMultipleComponent]
    public sealed class GameInitialiser : MonoBehaviour
    {
        [SF] private GameManager _manager = null;
        [SF] private GameObject _player = null;

        private static GameInitialiser s_active = null;

// INITIALISATION

        /// <summary>
        /// Initialises the input manager
        /// </summary>
        private void Awake(){
            if (s_active == null){
                _manager.Initialise(_player);
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