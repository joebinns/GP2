using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.UI;

namespace GameProject.HUD
{
    [RequireComponent(typeof(Image))]
    public class Reticle : MonoBehaviour
    {
        [SF] private Image _reticle;
        [Space]
        [SF] private Sprite _default;
        
        public void SetReticle(Sprite sprite) {
            _reticle.sprite = sprite;
        }
        
        public void ResetReticle() {
            _reticle.sprite = _default;
        }
    }
}
