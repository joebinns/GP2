using SF = UnityEngine.SerializeReference;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace GameProject.Interactions
{
    public class UIDisplayInteraction : BaseInteraction
    {
        [SF] private Color _success = Color.green;
        [SF] private Color _failure = Color.red;
        [Space]
        [SF] private Image _image = null;
        [SF] private TMP_Text _text = null;
        [SF] private float _flashDuration = 0.25f;

        private Color _default;

// INITIALISATION

        private void Awake() {
            _default = _image.color;
        }
        
// DISPLAY HANDLING

        /// <summary>
        /// Changes the display image colour
        /// </summary>
        public override void Win(){
            _image.color = _success;
        }

        /// <summary>
        /// Changes the display image colour
        /// </summary>
        public override void Lose()
        {
            StartCoroutine(FlashColour());
        }

        private IEnumerator FlashColour(){
            _image.color = _failure;
            yield return new WaitForSeconds(_flashDuration);
            _image.color = _default;
        }

        /// <summary>
        /// Changes the display text
        /// </summary>
        public override void Changed(string numbers){
            _text.text = numbers;
        }
    }
}