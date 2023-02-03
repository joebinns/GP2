using SF = UnityEngine.SerializeField;
using UnityEngine;
using GameProject.Cameras;
using GameProject.Levels;
using GameProject.Scenes;
using GameProject.Game;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace GameProject.Interface
{
    public class UIPlayerSelection : MonoBehaviour
    {
        [SF] private LevelManager _levels  = null;
        [SF] private ScenesManager _scenes = null;
        [SF] private GameManager _game = null;
        [Space]
        [SF] private LevelInfo[] _rooms = null; // TEMPORARY FOR FIRST RELEASE
        [Space]
        [SF] private UnityEvent _onControlLoaded = new();
        [SF] private UnityEvent _onShipLoaded    = new();

        private LevelInfo _room = null; // TEMPORARY
        private Distributor<LevelInfo> _available = null;

// INITIALISATION

        /// <summary>
        /// Initialises available rooms
        /// </summary>
        private void Awake(){
            _available = new Distributor<LevelInfo>(_rooms);
        }

// PLAYER HANDLING

        /// <summary>
        /// Loads the control room level
        /// </summary>
        public void SelectControlRoom(){
            //_levels.SubscribeOnLoaded(OnControlRoomLoaded);
            //_levels.LoadLevel(_levels.ControlRoom);

            // TEMPORARY
            if (_room != null && _scenes.IsLoaded(_room.BuildIndex)){
                _scenes.SubscribeOnUnloaded(SelectControlRoom);
                _scenes.UnloadScene(_room.BuildIndex);

            } else {  
                _levels.SubscribeOnLoaded(OnControlRoomLoaded);
                _levels.LoadLevel(_levels.ControlRoom);
            }
        }

        /// <summary>
        /// On level manager, level loaded callback
        /// </summary>
        private void OnControlRoomLoaded(){
            _levels.UnsubscribeOnLoaded(
                OnControlRoomLoaded
            );
            _onControlLoaded.Invoke();
        }


        /// <summary>
        /// Loads the spaceship level
        /// </summary>
        public void SelectSpaceship(){
            _levels.SubscribeOnLoaded(OnSpaceshipLoaded);
            _levels.LoadLevel(_levels.Spaceship);
        }

        /// <summary>
        /// On level manager, level loaded callback
        /// </summary>
        private void OnSpaceshipLoaded(){
            _levels.UnsubscribeOnLoaded(
                OnSpaceshipLoaded
            );

            // TEMPORARY
            if (_room != null && _scenes.IsLoaded(_room.BuildIndex)){
                _scenes.SubscribeOnUnloaded(LoadRoom);
                _scenes.UnloadScene(_room.BuildIndex);
            
            } else LoadRoom();
        }


        /// <summary>
        /// TEMPORARY loads a random room 
        /// </summary>
        private void LoadRoom(){
            _scenes.UnsubscribeOnUnloaded(LoadRoom);

            _room = _available.GetNext();
            _scenes.SubscribeOnLoaded(OnRoomLoaded);
            _scenes.LoadScene(_room.BuildIndex);
        }

        /// <summary>
        /// TEMPORARY On level manager, level loaded callback
        /// </summary>
        private void OnRoomLoaded(){
            _scenes.UnsubscribeOnUnloaded(
                OnRoomLoaded
            );
            _onShipLoaded.Invoke();
        }
    }
}