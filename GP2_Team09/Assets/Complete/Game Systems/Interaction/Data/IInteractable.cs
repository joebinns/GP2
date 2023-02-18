using SF = UnityEngine.SerializeField;

namespace GameProject.Interactions
{
    public interface IInteractable
    {
        public InteractableType InteractableType { get; }
        public Outline Outline { get; }
        public void Perform(bool interacting);
    }
}