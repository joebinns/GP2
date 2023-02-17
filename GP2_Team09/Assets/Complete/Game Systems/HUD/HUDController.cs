using System;
using SF = UnityEngine.SerializeField;
using GameProject.Interactions;
using UnityEngine;

namespace GameProject.HUD
{
    public abstract class HUDController : MonoBehaviour
    {
        [SF] private PlayerInteract _playerInteract;
        [SF] private Reticle _reticleScript;
        [SF] private Prompt _promptScript;
        [Space]
        [SF] private InteractableSettings _holdSettings;
        [SF] private InteractableSettings _torsionalGrabSettings;
        [SF] private InteractableSettings _linearGrabSettings;
        [SF] private InteractableSettings _pressSettings;
        
        private InteractableType _type;
        private InteractableMode _mode;

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

        private InteractableSettings GetSettings(InteractableType type) {
            InteractableSettings settings = null;
            switch (_type) {
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
        
        private (Sprite, PromptInfo) GetInfo(InteractableSettings settings, InteractableMode mode) {
            (Sprite, PromptInfo) info = (null, null);
            switch (_mode) {
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
