using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    public interface IInteractable
    {
        void Trigger();
    }
}