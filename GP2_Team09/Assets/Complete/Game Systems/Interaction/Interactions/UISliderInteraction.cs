using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.UI;
using GameProject.Interactions;

namespace GameProject.Interface
{
    public class UISliderInteraction : BaseInteraction
    {
        [SF] private float _minValue = 0;
        [SF] private float _maxValue = 10;
        [SF] private Color _lowValue = Color.red;
        [SF] private Color _highValue = Color.green;

        [SF] private Image  _fill   = null;
        [SF] private Slider _slider = null;

        [Space, SF] private BaseInteraction _owner = null;

        /// <summary>
        /// Assigns the value to the slider
        /// </summary>
        public override void Changed(BaseInteraction sender, float value){
            if (sender != _owner) return;
            _slider.value = value;

            var lerp = Mathf.InverseLerp(_minValue, _maxValue, value);
            _fill.color = Color.Lerp(_lowValue, _highValue, lerp);
        }
    }
}