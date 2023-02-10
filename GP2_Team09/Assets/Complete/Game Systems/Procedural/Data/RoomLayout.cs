using HIDE = UnityEngine.HideInInspector;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject.Procedural
{
    [CreateAssetMenu(fileName = "Room Layout", menuName = "Procedural/Room Layout")]
    public class RoomLayout : ScriptableObject
    {
        [SF] private GameplaySetup[] _gameplay = null;

        [SF, HIDE] private List<ObjectData> _layout  = null;
		[SF, HIDE] private List<LightData>  _lights  = null;
        [SF, HIDE] private List<EffectData> _effects = null;
        [SF, HIDE] private List<ReflectionData> _reflections = null;

// PROPERTIES

        public List<ObjectData> Layout => _layout;
        public List<LightData>  Lights => _lights;
        public List<EffectData> Effects => _effects;
        public List<ReflectionData> Reflections => _reflections;

// LAYOUT HANDLING

        /// <summary>
        /// Assigns the layout data
        /// </summary>
        public void AssignData(List<ObjectData> layout, List<LightData> lights,
        List<EffectData> effects, List<ReflectionData> reflections){
            _layout      = layout;
            _lights      = lights;
            _effects     = effects;
            _reflections = reflections;
        }
    }
}