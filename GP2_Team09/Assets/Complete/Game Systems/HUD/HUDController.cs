using GameProject.Interactions;
using UnityEngine;

namespace GameProject.HUD
{
    public abstract class HUDController : MonoBehaviour
    {
        [SerializeField] private Reticle _reticleScript;
        [SerializeField] private Prompt _promptScript;
        [Space]
        [SerializeField] private InteractableSettings _holdSettings;
        [SerializeField] private InteractableSettings _torsionalGrabSettings;
        [SerializeField] private InteractableSettings _linearGrabSettings;
        [SerializeField] private InteractableSettings _pressSettings;
        
        private InteractableType _type;
        private InteractableMode _mode;

        /// <summary>
        /// Switch the reticle sprite and displayed prompt based on the passed interactable type and interactable mode parameters
        /// </summary>
        public void SwitchInteractable(InteractableType type, InteractableMode mode) {
            if (type == _type && mode == _mode) return;

            _type = type;
            _mode = mode;
            
            var settings = GetSettings(_type);
            var (reticle, prompt) = GetInfo(settings, _mode);

            if (reticle != null) _reticleScript.SetReticle(reticle);
            else _reticleScript.ResetReticle();
            
            if (prompt == null || prompt.IsNull()) _promptScript.TogglePrompt(false);
            else _promptScript.SetPrompt(prompt);
        }

        /// <summary>
        /// Get the appropriate interactable settings based on the passed interactable type parameter
        /// </summary>
        private InteractableSettings GetSettings(InteractableType type) {
            InteractableSettings settings = null;
            switch (type) {
                case InteractableType.None:
                    settings = null;
                    break;
                case InteractableType.Hold:
                    settings = _holdSettings;
                    break;
                case InteractableType.LinearGrab:
                    settings = _linearGrabSettings;
                    break;
                case InteractableType.TorsionalGrab:
                    settings = _torsionalGrabSettings;
                    break;
                case InteractableType.Press:
                    settings = _pressSettings;
                    break;
            }
            return settings;
        }
        
        /// <summary>
        /// Get the appropriate reticle sprite and prompt info to display based on the passed interactable settings and interactable mode parameters
        /// </summary>
        private (Sprite, PromptInfo) GetInfo(InteractableSettings settings, InteractableMode mode) {
            (Sprite, PromptInfo) info = (null, null);
            switch (mode) {
                case InteractableMode.None:
                    info = (null, null);
                    break;
                case InteractableMode.Hover:
                    info = (settings?.HoverReticle, settings?.HoverPromptInfo);
                    break;
                case InteractableMode.Action:
                    info = (settings?.ActionReticle, settings?.ActionPromptInfo);
                    break;
            }
            return info;
        }
    }
}
