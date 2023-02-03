using Action = System.Action<float>;
using System.Collections;
using UnityEngine;

namespace GameProject.Updates
{
    [CreateAssetMenu(fileName = "Update Manager", 
     menuName = "Managers/Update")]
    public sealed class UpdateManager : ScriptableObject
    {
        public float TimeScale = 1f;
        private UpdateRelay _relay = null;

        private Subscription<Action> _update      = null;
        private Subscription<Action> _fixedUpdate = null;
        private Subscription<Action> _lateUpdate  = null;

// PROPERTIES

        /// <summary>
        /// Toggles this manager's update relay
        /// </summary>
        public bool Paused {
            get => !_relay.enabled;
            set => _relay.enabled = !value;
        }

// INITIALISATION

        /// <summary>
        /// Initialises the update manager
        /// </summary>
        public void Initialise(UpdateRelay relay, int capacity, bool sorted = false){
            _update      = new Subscription<Action>(capacity, sorted);
            _fixedUpdate = new Subscription<Action>(capacity, sorted);
            _lateUpdate  = new Subscription<Action>(capacity, sorted);
            _relay = relay;
        }

        /// <summary>
        /// Deinitialise the update manager
        /// </summary>
        public void OnDestroy(){
            _update.ClearSubscribers();
            _fixedUpdate.ClearSubscribers();
            _lateUpdate.ClearSubscribers();
        }

// UPDATES

        /// <summary>
        /// Notifies subscribers, on regular update
        /// </summary>
        public void OnUpdate(float deltaTime){
            _update.NotifySubscribers(deltaTime * TimeScale);
        }

        /// <summary>
        /// Notifies subscribers, on fixed update
        /// </summary>
        public void OnFixedUpdate(float fixedDeltaTime){
            _fixedUpdate.NotifySubscribers(fixedDeltaTime * TimeScale);
        }

        /// <summary>
        /// Notifies subscribers, on late update
        /// </summary>
        public void OnLateUpdate(float deltaTime){
            _lateUpdate.NotifySubscribers(deltaTime * TimeScale);
        }

// COROUTINES

        /// <summary>
        /// Stops the specified coroutine
        /// </summary>
        public void StopCoroutine(Coroutine routine){
            if (routine == null) return;
            _relay.StopCoroutine(routine);
        }

        /// <summary>
        /// Starts a new coroutine
        /// </summary>
        public Coroutine StartCoroutine(IEnumerator routine){
            if (routine == null) return null;
            return _relay.StartCoroutine(routine);
        }

// SUBSCRIPTIONS

        /// <summary>
        /// Adds the subscriber to the update manager
        /// </summary>
        public void Subscribe(Action subscriber, UpdateType type){
            Subscribe(subscriber, type, 0);
        }

        /// <summary>
        /// Adds the subscriber to the update manager
        /// </summary>
        public void Subscribe(Action subscriber, UpdateType type, int priority){
            switch (type){
                case UpdateType.Update:
                    if(_update.Sorted)
                         _update.AddSubscriber(subscriber, priority);
                    else _update.AddSubscriber(subscriber);
                    break;

                case UpdateType.FixedUpdate:
                    if (_fixedUpdate.Sorted)
                         _fixedUpdate.AddSubscriber(subscriber, priority);
                    else _fixedUpdate.AddSubscriber(subscriber);
                    break;

                case UpdateType.LateUpdate:
                    if (_lateUpdate.Sorted)
                         _lateUpdate.AddSubscriber(subscriber, priority);
                    else _lateUpdate.AddSubscriber(subscriber);
                    break;

                default: MissingType(type); break;
            }
        }

        /// <summary>
        /// Removes the subscriber from the update manager
        /// </summary>
        public void Unsubscribe(Action subscriber, UpdateType type){
            switch (type){
                case UpdateType.Update:
                    _update.RemoveSubscriber(subscriber);
                    break;

                case UpdateType.FixedUpdate:
                    _fixedUpdate.RemoveSubscriber(subscriber);
                    break;

                case UpdateType.LateUpdate:
                    _lateUpdate.RemoveSubscriber(subscriber);
                    break;

                default: MissingType(type); break;
            }
        }

// ERRORS

        /// <summary>
        /// Logs a missing update type error to the console
        /// </summary>
        private void MissingType(UpdateType type){
            Debug.LogError(new System.MissingFieldException(
                $"Missing UpdateType.{type} in Update Manager"
            ));
        }
    }
}