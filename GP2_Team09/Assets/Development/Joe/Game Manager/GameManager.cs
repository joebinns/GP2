using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Cameras;

namespace GameProject.Game
{
    [CreateAssetMenu(fileName = "Game Manager", menuName = "Managers/Game")]
    public sealed partial class GameManager : ScriptableObject
    {
        [SF] private GameObject _engineerPrefab = null;
        [SF] private GameObject _crewPrefab = null;
        [Space]
        [SF] private CameraManager _camera = null;

// INITIALISATION

        /// <summary>
        /// Initialises the game manager
        /// </summary>
        public void Initialise(){}

        /// <summary>
        /// Deinitialise the game manager
        /// </summary>
        public void OnDestroy(){}

// PLAYER HANDLING

        /// <summary>
        /// Spawns the engineer player
        /// </summary>
        public void SpawnEngineer() => SpawnPlayer(_engineerPrefab);

        /// <summary>
        /// Spawns the engineer player
        /// </summary>
        public void SpawnCrewmember() => SpawnPlayer(_crewPrefab);

        /// <summary>
        /// Spawns the specified player
        /// </summary>
        private void SpawnPlayer(GameObject prefab){
            var player = Instantiate(prefab);
            var camTarget = player.transform.GetChild(0);
            
            _camera.SetTarget(null, camTarget);
            _camera.Camera.gameObject.SetActive(true);
        }
    }
}