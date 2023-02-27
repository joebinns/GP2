using SF = UnityEngine.SerializeField;
using UnityEngine.UI;
using UnityEngine;

namespace GameProject.Interactions
{
    public class UITimeSliderInteraction : BaseInteraction
    {
        [SF] private Slider _slider = null;


        public override void Restore(){
            _slider.value = _slider.maxValue;
        }

        public override void Changed(float value){
            _slider.value = value;
        }

        public override void Changed(BaseInteraction sender, float value){
            _slider.value = value;
        }
    }
}