using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;
using System.Collections.Generic;

namespace GameProject.Environment
{
    public class DoorInteraction : BaseInteraction
    {
        [SF] private bool _locked = false;
        [SF] private bool _opened = false;
        [SF] private Animator _animator = null;

        [Space, SF] private List<ActionInfo> _onLocking   = null;
        [Space, SF] private List<ActionInfo> _onUnlocking = null;
        [Space, SF] private List<ActionInfo> _onOpening = null;
        [Space, SF] private List<ActionInfo> _onClosing = null;

        private readonly int OPEN_HASH = Animator.StringToHash("Open");

// PROPERTIES

        public bool Locked => _locked;

// INITIALISATION

        /// <summary>
        /// Initialises the door
        /// </summary>
        private void Awake(){
            _animator.SetBool(OPEN_HASH, _opened);
        }

// DOOR HANDLING

        /// <summary>
        /// Locks the door
        /// </summary>
        public override void Lock(){
            if (_locked) return;

            _locked = true;
            Interact(_onLocking);
        }

        /// <summary>
        /// Unlocks the door
        /// </summary>
        public override void Unlock(){
            if (!_locked) return;

            _locked = false;
            Interact(_onUnlocking);
        }


        /// <summary>
        /// Opens the door
        /// </summary>
        public override void Open(){
            if (_locked || _opened) return;

            _opened = true;
            _animator.SetBool(OPEN_HASH, _opened);

            Interact(_onOpening);
        }

        /// <summary>
        /// Closes the door
        /// </summary>
        public override void Close(){
            if (_locked || !_opened) return;

            _opened = false;
            _animator.SetBool(OPEN_HASH, _opened);

            Interact(_onClosing);
        }
    }
}
