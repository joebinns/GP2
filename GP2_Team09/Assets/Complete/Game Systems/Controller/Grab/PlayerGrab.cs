using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.Grab
{
    public abstract class PlayerGrab : MonoBehaviour
    {
        [SF] protected Transform _cameraTarget;
        
        protected GrabInteraction _grabbing;
        protected bool IsGrabbing => _grabbing != null;
        
// INITIALISATION
        
        /// <summary>
        /// Initialise the player hold and hold pivot references on all holdable game objects
        /// </summary>
        protected virtual void Start() {
        }

// HOLD HANDLING

        /// <summary>
        /// Grab the object to hold
        /// </summary>
        public virtual void Grab(GrabInteraction toGrab) {
            SetGrab(toGrab);
        }

        /// <summary>
        /// Release any held object
        /// </summary>
        public virtual void Release() {
            SetGrab(null);
        }

        /// <summary>
        /// Sets the appropriate components controlling a game objects hold behaviour on or off as appropriate
        /// </summary>
        protected abstract void SetGrab(GrabInteraction toGrab);
    }
}
