using SF = UnityEngine.SerializeReference;
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
        public override void Lose(){
            _image.color = _failure;
        }

        /// <summary>
        /// Changes the display text
        /// </summary>
        public override void Changed(string numbers){
            _text.text = numbers;
        }
    }
}