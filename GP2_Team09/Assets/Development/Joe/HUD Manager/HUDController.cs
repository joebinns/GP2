using System.Collections.Generic;
using GameProject.Interactions;
using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.HUD
{
    public abstract class HUDController : MonoBehaviour
    {
        [SF] private PlayerInteract _playerInteract;
        [SF] private Reticle _reticle;

        // TODO: Call Reticle.cs to switch the reticle based on interactions
        private void OnEnable() {
            _playerInteract.OnActionsChanged += SwitchReticle;
        }

        private void OnDisable() {
            _playerInteract.OnActionsChanged -= SwitchReticle;
        }
        
        private void SwitchReticle(List<IInteractable> actions) {
            //Debug.Log(actions.Count);
            // TODO: Get closest action
        }

        private void GetClosestAction(List<IInteractable> actions) {
            var nearestDistance = Mathf.Infinity;
            foreach (var action in actions) {
                //if (action.) // TODO: Can't get transform of IInteractable...
            }
        }
    }
}
