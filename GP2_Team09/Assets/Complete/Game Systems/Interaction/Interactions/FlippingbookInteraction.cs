using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace GameProject.Interactions
{
    public class FlippingbookInteraction : BaseInteraction
    {
        [SF] private Image _image = null;
        [Space, SF] private Sprite[] _sprites = null;
        [Space, SF] private List<ActionInfo> _onChange = null;

        private int _index = 0;
        private Sprite _current = null;

// INITIALISATION

        /// <summary>
        /// Initialises the flipbook
        /// </summary>
        private void Start(){
            _image.sprite = _sprites[_index];

            Interact(_onChange, _index);
            Interact(_onChange, _current);
        }

// FLIPBOOK HANDLING

        /// <summary>
        /// Enables the flipbook
        /// </summary>
        public override void Enable(){
            this.enabled = true;
            _image.enabled = true;
        }

        /// <summary>
        /// Disables the flipbook
        /// </summary>
        public override void Disable(){
            this.enabled = false;
            _image.enabled = true;
        }

        /// <summary>
        /// Resets the flipbook
        /// </summary>
        public override void Restore(){
            _index = 0;
            _current = _sprites[0];

            if (_image) _image.sprite = _current;

            Interact(_onChange, _index);
            Interact(_onChange, _current);
        }

        /// <summary>
        /// Changes the flipbook to next sprite
        /// </summary>
        public override void Increment(){
            if (!this.enabled) return;

            _current = _sprites[
                ++_index % _sprites.Length
            ];

            if (_image) _image.sprite = _current;

            Interact(_onChange, _index);
            Interact(_onChange, _current);
        }

        /// <summary>
        /// Changes the flipbook to previous sprite
        /// </summary>
        public override void Decrement(){
            if (!this.enabled) return;

            _index   = (_index + _sprites.Length - 1) % _sprites.Length;
            _current = _sprites[_index];

            if (_image) _image.sprite = _current;

            Interact(_onChange, _index);
            Interact(_onChange, _current);
        }

        /// <summary>
        /// Changes the flipbook sprite to value
        /// </summary>
        public override void Changed(float value){
            if (!this.enabled) return;

            _index = Mathf.Clamp(
                (int)value, 0, _sprites.Length - 1
            );

            _current = _sprites[_index];
            if (_image) _image.sprite = _current;

            Interact(_onChange, _index);
            Interact(_onChange, _current);
        }
    }
}