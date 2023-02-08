using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Procedural
{
    [CreateAssetMenu(fileName = "Gameplay Setup", menuName = "Procedural/Gameplay Setup")]
    public class GameplaySetup : ScriptableObject
    {
		[SF] private List<SimpleButtonData> _buttons = null;

// PROPERTIES

        public List<SimpleButtonData> Buttons => _buttons;

// LAYOUT HANDLING

        /// <summary>
        /// Assigns the gameplay data
        /// </summary>
        public void AssignData(List<SimpleButtonData> buttons){
            _buttons = buttons;
        }
    }
}