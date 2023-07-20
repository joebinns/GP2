using UnityEngine;

namespace GameProject.Interactions
{
    [CreateAssetMenu(fileName = "Interactable Settings", menuName = "Settings/Interactable Settings")]
    public class InteractableSettings : ScriptableObject
    {
        [Header("Reticle")]
        [SerializeField] private Sprite _hoverReticle;
        [SerializeField] private Sprite _actionReticle;
        [Header("Prompt")]
        [SerializeField] private PromptInfo _hoverPromptInfo;
        [SerializeField] private PromptInfo _actionPromptInfo;

// PROPERTIES
        
        public Sprite HoverReticle => _hoverReticle;
        public Sprite ActionReticle => _actionReticle;
        public PromptInfo HoverPromptInfo => _hoverPromptInfo;
        public PromptInfo ActionPromptInfo => _actionPromptInfo;
    }
}
