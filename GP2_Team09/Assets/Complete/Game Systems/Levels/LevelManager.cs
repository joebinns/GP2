using NS = System.NonSerializedAttribute;
using SF = UnityEngine.SerializeField;
using Action = System.Action;
using UnityEngine;
using GameProject.Scenes;
using GameProject.Interface;

namespace GameProject.Levels
{
    [CreateAssetMenu(fileName = "Level Manager", menuName = "Managers/Levels")]
    public class LevelManager : ScriptableObject
    {
        [SF] private LevelInfo _startup   = null;
        [SF] private LevelInfo _control   = null;
        [SF] private LevelInfo _spaceship = null;
        [Space]
        [SF] private ScenesManager _scene = null;
        [SF] private MenuManager _menu    = null;

        private LevelInfo _current = null;

        [NS] private bool _loading = false;
        private Subscription<Action> _onLoaded = null;

// PROPERTIES

        public LevelInfo Startup => _startup;
        public LevelInfo ControlRoom => _control;
        public LevelInfo Spaceship => _spaceship;
        public LevelInfo Current => _current;

// INITIALISATION

        /// <summary>
        /// Initialises the level manager
        /// </summary>
        public void Initialise(){
            _onLoaded = new Subscription<Action>();
        }

        /// <summary>
        /// Finalises the level manager
        /// </summary>
        public void OnDestroy(){
            _onLoaded.ClearSubscribers();
            _current = null;
        }

// LEVEL MANAGEMENT

        /// <summary>
        /// Loads the specified level
        /// </summary>
        public void LoadLevel(LevelInfo level){
            if (_loading) return;

            if (_current != null && _scene.IsLoaded(_current.BuildIndex)){
                _scene.SubscribeOnUnloaded(LoadNext);
                _scene.UnloadScene(_current.BuildIndex);

                _current = level;
                _loading = true;

            } else {
                _current = level;
                LoadNext();
            }
        }

        /// <summary>
        /// Loads the next level
        /// </summary>
        private void LoadNext(){
            if (!_scene.IsLoaded(_current.BuildIndex)){
                _scene.SubscribeOnLoaded(OnLevelLoaded);
                _scene.LoadScene(_current.BuildIndex, true);
                _loading = true;

            } else UnloadPrevious();
        }

        /// <summary>
        /// On scene manager, level loaded event
        /// </summary>
        private void OnLevelLoaded(){
            _onLoaded.NotifySubscribers();
            _loading = false;
        }


        /// <summary>
        /// Unloads previously loaded level
        /// </summary>
        private void UnloadPrevious(){
            _scene.SubscribeOnUnloaded(OnPreviousUnloaded);
            _scene.UnloadScene(_current.BuildIndex);
            _loading = true;
        }

        /// <summary>
        /// On scene manager, level unloaded event
        /// </summary>
        private void OnPreviousUnloaded(){
            _scene.UnsubscribeOnUnloaded(OnPreviousUnloaded);
            _loading = false;

            LoadNext();
        }

// SUBSCRIPTIONS

        /// <summary>
        /// Subscribes to the on selected level loaded event
        /// </summary>
        public void SubscribeOnLoaded(Action subscriber)
            => _onLoaded.AddSubscriber(subscriber);

        /// <summary>
        /// Unsubscribes from the on selected level loaded event
        /// </summary>
        public void UnsubscribeOnLoaded(Action subscriber)
            => _onLoaded.RemoveSubscriber(subscriber);
    }
}