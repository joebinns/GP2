using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.HUD
{
    public abstract class HUDController : MonoBehaviour
    {
        [SF] private PlayerInteract _playerInteract;
        [SF] private Reticle _reticleScript;

        private Sprite _reticle;

        public void SwitchReticle(Sprite reticle) {
            if (_reticle == reticle) return;
            
            _reticle = reticle;
            if (_reticle != null) _reticleScript.SetReticle(_reticle);
            else _reticleScript.ResetReticle();
            
        }
    }
}
