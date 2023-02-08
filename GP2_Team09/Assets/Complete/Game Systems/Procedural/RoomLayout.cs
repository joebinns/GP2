using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Procedural
{
    [CreateAssetMenu(fileName = "Room Layout", menuName = "Procedural/Room Layout")]
    public class RoomLayout : ScriptableObject
    {
        [SF] private List<ObjectData> _layout = null;
		[SF] private List<LightData>  _lights = null;
        [SF] private List<ReflectionData> _reflections = null;

		[SF] private List<SimpleButtonData> _buttons = null;

// PROPERTIES

        public List<ObjectData> Layout => _layout;
        public List<LightData>  Lights => _lights;
        public List<ReflectionData> Reflections => _reflections;

        public List<SimpleButtonData> Buttons => _buttons;

// LAYOUT HANDLING

        /// <summary>
        /// Assigns the layout data
        /// </summary>
        public void AssignData(List<ObjectData> layout, List<LightData> lights, 
        List<ReflectionData> reflections, List<SimpleButtonData> buttons){
            _layout = layout;
            _lights = lights;
            _reflections = reflections;
            _buttons = buttons;
        }
    }
}