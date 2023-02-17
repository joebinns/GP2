using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    public class ToggleInteraction : BaseInteraction
    {
        [SF] private GameObject _target = null;

        public override void Show() => _target.SetActive(true);
        public override void Hide() => _target.SetActive(false);
    }
}