using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    public interface IInteractable
    {
        public Sprite HoverReticle { get; }
        public Sprite ActionReticle { get; }
        public Outline Outline { get; }
        public void Perform(bool interacting);
    }
}