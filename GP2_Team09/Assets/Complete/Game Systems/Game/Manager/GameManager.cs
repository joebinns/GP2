using SF = UnityEngine.SerializeField;
using Random = System.Random;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using GameProject.Cameras;
using GameProject.Inputs;
using GameProject.HUD;
using GameProject.Updates;
using GameProject.Levels;
using GameProject.Interface;

namespace GameProject.Game
{
    [CreateAssetMenu(fileName = "Game Manager", menuName = "Managers/Game")]
    public sealed partial class GameManager : ScriptableObject
    {
        [Header("Player")]
        [SF] private int _lives = 3;
        [SF] private float _lostReturnDelay = 3;
        [SF] private float _wonReturnDelay  = 5;

        [Header("References")]
        [SF] private GameObject _engineerPrefab = null;
        [SF] private GameObject _crewPrefab     = null;
        [SF] private ShipNames  _shipNames      = null;

        [Header("Managers")]
        [SF] private CameraManager _camera = null;
        [SF] private InputManager  _input  = null;
        [SF] private HUDManager    _hud    = null;
        [SF] private UpdateManager _update = null;
        [SF] private LevelManager  _level  = null;
        [SF] private MenuManager   _menu   = null;

        private int _health = 3;
        private string _ship = string.Empty;
        private Random _random = null;

// PROPERTIES

        public string ShipName => _ship;
        public Random Random => _random;
        public GameObject Player { get; private set; } = null;

// INITIALISATION

        /// <summary>
        /// Initialises the game manager
        /// </summary>
        public void Initialise(GameObject player) {
            InitPlayer(player);
            InitShipName();

            _health = _lives;
            _hud?.UpdateHealth(_health);
            _menu?.ShowMenu(false);
        }

        /// <summary>
        /// Deinitialise the game manager
        /// </summary>
        public void OnDestroy(){}


        /// <summary>
        /// Initialises the player and camera target
        /// </summary>
        private void InitPlayer(GameObject player){
            Player = player;

            var target = Player.GetComponentInChildren<CameraTarget>();
            _camera.SetTarget(null, target.transform);
        }

        /// <summary>
        /// Initialises the ship name
        /// </summary>
        private void InitShipName(){
            if (_shipNames == null) return;

            var random = new Random((int)Time.time);
            var names  = _shipNames.Names;
            var index  = random.Next(0, names.Length);

            SetShipName(names[index]);
        }

// RANDOMISATION

        /// <summary>
        /// Assigns the ship name and initialises the randomisation
        /// </summary>
        public void SetShipName(string name){
            if (InvalidShipName(name)) return;

            _ship = name;
            var chars = _ship.ToLower();
            var key   = string.Empty;

            for (int i = 0; i < chars.Length; i++)
                key = string.Concat(key, (int)chars[i]);

            if (int.TryParse(key, out var seed))
                _random = new Random(seed);
        }

        /// <summary>
        /// Returns if the ship name is valid or not
        /// </summary>
        private bool InvalidShipName(string name){
            if (!string.IsNullOrEmpty(name)) return false;

            Debug.LogError($"The name, {name}, is not a valid ship name");
            return true;
        }

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
            _hud.UpdateHealth(--_health);
            if (_health > 0) return;

            _hud.DisplayLost(true);
            SetPlayerInput(false);

            _update.StartCoroutine(
                ReturnToMenu(_lostReturnDelay)
            );
        }

// LEVEL HANDLING

        /// <summary>
        /// Returns the player to the main menu scene after delay
        /// </summary>
        private IEnumerator ReturnToMenu(float delay){
            yield return new WaitForSeconds(delay);

            _level.SubscribeOnLoaded(OnMenuLoaded);
            _level.LoadLevel(_level.Startup);
        }

        /// <summary>
        /// Shows menu on main menu scene loaded
        /// </summary>
        private void OnMenuLoaded(){
            _level.UnsubscribeOnLoaded(OnMenuLoaded);
            _menu?.ShowMenu(true);
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