using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Outlines
{
    public class OscillatingOutline : Outline {
        [Header("Width Oscillations")]
        [SF] private float _range = 4f;
        [SF] private float _rate = 4f;

        private float _initialWidth;

        protected override void Awake() {
            base.Awake();
            _initialWidth = OutlineWidth;
        }
        
        protected override void Update() {
            base.Update();
            OutlineWidth = _initialWidth + Mathf.Sin(Time.time * _rate) * _range * 0.5f;
        }
    }
}
