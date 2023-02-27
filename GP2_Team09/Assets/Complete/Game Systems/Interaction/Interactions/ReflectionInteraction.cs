using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    public class ReflectionInteraction : BaseInteraction
    {
        [SF] private bool _bakeOnStart = true;
        [SF] private ReflectionProbe[] _probes = null;

        /// <summary>
        /// Initialise reflections on start
        /// </summary>
        private void Start(){
            if (_bakeOnStart) Trigger();
        }

        /// <summary>
        /// Updates the reflection probe
        /// </summary>
        public override void Trigger(){
            for (int i = 0; i < _probes.Length; i++){
                _probes[i]?.RenderProbe();
            }
        }
    }
}