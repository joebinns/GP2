using SS = System.SerializableAttribute;
using UnityEngine;

namespace GameProject.Interactions {
    [SS] public struct ActionInfo {
        public BaseInteraction Target;
        public InteractionType Action;
    }
}