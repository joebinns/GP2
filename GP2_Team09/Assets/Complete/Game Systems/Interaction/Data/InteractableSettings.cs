using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    [CreateAssetMenu(fileName = "Interactable Settings",
        menuName = "Settings/Interactable Settings")]
    public class InteractableSettings : ScriptableObject
    {
        [Header("Reticle")]
        [SF] private Sprite _hoverReticle;
        [SF] private Sprite _actionReticle;
        [Header("Prompt")]
        [SF] private PromptInfo _hoverPromptInfo;
        [SF] private PromptInfo _actionPromptInfo;

// PROPERTIES
        
        public Sprite HoverReticle => _hoverReticle;
        public Sprite ActionReticle => _actionReticle;
        public PromptInfo HoverPromptInfo => _hoverPromptInfo;
        public PromptInfo ActionPromptInfo => _actionPromptInfo;
    }
}
