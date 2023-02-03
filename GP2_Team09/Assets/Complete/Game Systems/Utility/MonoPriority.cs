using UnityEngine;
using UnityEngine.UIElements;

namespace GameProject {
    public class MonoPriority : MonoBehaviour
    {
        private int _priority = -1;

        public int Priority {
            get => GetPriority();
            set => _priority = value;
        }

        private int GetPriority(){
            if (_priority > -1) 
                return _priority;

            var comp = GetComponents<MonoPriority>();
            var length = comp.Length;

            for (int i = 0; i < length; i++){
                comp[i].Priority = length - i;
            }

            return _priority;
        }
    }
}