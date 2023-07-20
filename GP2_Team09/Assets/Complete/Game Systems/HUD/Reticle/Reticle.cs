using UnityEngine;
using UnityEngine.UI;

namespace GameProject.HUD
{
    [RequireComponent(typeof(Image))]
    public class Reticle : MonoBehaviour
    {
        [SerializeField] private Image _reticle;
        [Space]
        [SerializeField] private Sprite _default;
        
        /// <summary>
        /// Set the sprite used by the reticle image
        /// </summary>
        public void SetReticle(Sprite sprite) {
            _reticle.sprite = sprite;
        }
        
        /// <summary>
        /// Reset the reticle image sprite to the default reticle
        /// </summary>
        public void ResetReticle() {
            _reticle.sprite = _default;
        }
    }
}
