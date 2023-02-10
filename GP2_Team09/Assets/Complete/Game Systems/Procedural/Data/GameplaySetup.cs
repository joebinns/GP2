using HIDE = UnityEngine.HideInInspector;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Procedural
{
    [CreateAssetMenu(fileName = "Gameplay Setup", menuName = "Procedural/Gameplay Setup")]
    public class GameplaySetup : ScriptableObject
    {
		[SF, HIDE] private List<InteractionData> _buttons = null;

// PROPERTIES

        public List<InteractionData> Interactables => _buttons;

// LAYOUT HANDLING

        /// <summary>
        /// Assigns the gameplay data
        /// </summary>
        public void AssignData(List<InteractionData> buttons){
            _buttons = buttons;
        }
    }
}