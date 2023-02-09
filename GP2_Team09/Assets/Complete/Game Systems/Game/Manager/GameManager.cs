using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Cameras;
using GameProject.HUD;

namespace GameProject.Game
{
    [CreateAssetMenu(fileName = "Game Manager", menuName = "Managers/Game")]
    public sealed partial class GameManager : ScriptableObject
    {
        [SF] private GameObject _engineerPrefab = null;
        [SF] private GameObject _crewPrefab = null;
        [Space]
        [SF] private CameraManager _camera = null;
        [SF] private HUDManager _hud = null; // temporary

// PROPERTIES
        public GameObject Player { get; private set; } = null;

// INITIALISATION

        /// <summary>
        /// Initialises the game manager
        /// </summary>
        public void Initialise(GameObject player) {
            Player = player;
            _camera.SetTarget(null, Player.GetComponentInChildren<CameraTarget>().transform);
        }

        /// <summary>
        /// Deinitialise the game manager
        /// </summary>
        public void OnDestroy(){}

        public void PlayerWin(){
            // TODO: Enable win HUD
        }
        
        public void PlayerLose(){
            // TODO: Enable lose HUD
        }

    }
}