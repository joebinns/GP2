using SF = UnityEngine.SerializeField;
using UnityEngine;
using System.Collections.Generic;

namespace GameProject.Interactions {
    public class SoundOnCollision : BaseInteraction
    {
        [SF] private List<ActionInfo> _onCollision = null;

        private void OnCollisionEnter(Collision collision){
            Interact(_onCollision);
        }
    }
}