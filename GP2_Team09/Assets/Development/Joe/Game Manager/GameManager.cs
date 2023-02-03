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
        private GameObject _player = null;

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
            DespawnPLayer();

            _player = Instantiate(prefab);
            var camTarget = _player.transform.GetChild(0);
            
            _camera.SetTarget(null, camTarget);
            _camera.Camera.gameObject.SetActive(true);
        }


        /// <summary>
        /// Removes the player from the world
        /// </summary>
        public void DespawnPLayer(){
            if (_player == null) return;

            // TEMPORARY
            _hud.DisplayCrew(false);
            _hud.DisplayEngineer(false);
            _hud.DisplayWon(false);
            _hud.DisplayLost(false);

            _camera.SetTarget(null, null);
            Destroy(_player);
        }
    }
}