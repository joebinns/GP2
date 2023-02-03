using SS = System.SerializableAttribute;
using UnityEngine.InputSystem;
using UnityEngine;

namespace GameProject.Inputs {
    [SS] public class InputInfo {
        public InputActionReference Input;

        [Tooltip("Event: Input started")]
        public bool Pressed;

        [Tooltip("Event: Input performed")]
        public bool Changed;

        [Tooltip("Event: Input ended")]
        public bool Released;

        [Tooltip("Mode: Sorts subscriptions by priority")]
        public bool Sorted;
    }
}