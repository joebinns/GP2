using SF = UnityEngine.SerializeField;
using System.Collections;
using UnityEngine;
using GameProject.Cameras;
using GameProject.Inputs;
using GameProject.HUD;
using GameProject.Updates;
using GameProject.Levels;

namespace GameProject.Game
{
    [CreateAssetMenu(fileName = "Game Manager", menuName = "Managers/Game")]
    public sealed partial class GameManager : ScriptableObject
    {
        [SF] private float _lostReturnDelay = 3;
        [SF] private float _wonReturnDelay  = 5;
        [Space]
        [SF] private GameObject _engineerPrefab = null;
        [SF] private GameObject _crewPrefab     = null;
        [Space]
        [SF] private CameraManager _camera = null;
        [SF] private InputManager  _input  = null;
        [SF] private HUDManager    _hud    = null;
        [SF] private UpdateManager _update = null;
        [SF] private LevelManager  _level  = null;

// PROPERTIES
        public GameObject Player { get; private set; } = null;

// INITIALISATION

        /// <summary>
        /// Initialises the game manager
        /// </summary>
        public void Initialise(GameObject player) {
            Player = player;

            var target = Player.GetComponentInChildren<CameraTarget>();
            _camera.SetTarget(null, target.transform);
        }

        /// <summary>
        /// Deinitialise the game manager
        /// </summary>
        public void OnDestroy(){}

// STATE HANDLING

        /// <summary>
        /// Triggers the win state
        /// </summary>
        public void PlayerWin(){
            _hud.DisplayWon(true);
            SetPlayerInput(false);

            _update.StartCoroutine(
                ReturnToMenu(_wonReturnDelay)
            );
        }
        
        /// <summary>
        /// Triggers the lose state
        /// </summary>
        public void PlayerLose(){
            _hud.DisplayLost(true);
            SetPlayerInput(false);

            _update.StartCoroutine(
                ReturnToMenu(_lostReturnDelay)
            );
        }

        /// <summary>
        /// Returns the player to the main menu scene after delay
        /// </summary>
        private IEnumerator ReturnToMenu(float delay){
            yield return new WaitForSeconds(delay);
            _level.LoadLevel(_level.Startup);
        }

// UTILITY

        /// <summary>
        /// Changes the player input state
        /// </summary>
        private void SetPlayerInput(bool enabled){
            _input.SetInputStates(_input.ActionGroup,   enabled);
            _input.SetInputStates(_input.MovementGroup, enabled);
        }
    }
}