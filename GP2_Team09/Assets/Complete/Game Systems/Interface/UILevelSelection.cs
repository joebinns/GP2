using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Levels;
using GameProject.Scenes;

namespace GameProject.Interface
{
    public class UILevelSelection : MonoBehaviour
    {
        [SF] private LevelManager _levels  = null;
        [SF] private ScenesManager _scenes = null;

// INITIALISATION

        public void Start() => SelectMainMenu();

// LEVEL LOADING

        /// <summary>
        /// Loads the main menu backdrop
        /// </summary>
        public void SelectMainMenu() => _levels.LoadLevel(_levels.Startup);

        /// <summary>
        /// Loads the control room level
        /// </summary>
        public void SelectControlRoom() => _levels.LoadLevel(_levels.ControlRoom);

        /// <summary>
        /// Loads the spaceship level
        /// </summary>
        public void SelectSpaceship() => _levels.LoadLevel(_levels.Spaceship);
    }
}