using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions {
    public class KeyInteraction : ButtonInteraction
    {
        [Header("Keypad")]
        [SF] private int key = 1;

// KEY HANDLING

        /// <summary>
        /// Triggers the key button on player interaction 
        /// </summary>
        public override void Perform(bool interacting){
            base.Perform(interacting);

            if (_pressed) Interact(_onPressed, key);
            else Interact(_onReleased, key);
        }
    }
}