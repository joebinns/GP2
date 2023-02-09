using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.UI;
using GameProject.Interactions;

namespace GameProject.Interface
{
    public class UISliderInteraction : BaseInteraction
    {
        [SF] private Slider _slider = null;
        [SF] private BaseInteraction _owner = null;

        /// <summary>
        /// Assigns the value to the slider
        /// </summary>
        public override void Changed(BaseInteraction sender, float value){
            if (sender != _owner) return;
            
            _slider.value = value;
        }
    }
}