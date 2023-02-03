using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    [CreateAssetMenu(fileName = "Interaction Settings",
     menuName = "Settings/Interaction Settings")]
    public class InteractionSettings : ScriptableObject
    {
        [Header("Interaction Settings")]
        [SF] private float _radius = 0.02f;
        [SF] private float _distance = 2f;
        [SF] private LayerMask _mask = 1 << 0;

// PROPERTIES

        public float Radius => _radius;
        public float Distance => _distance;
        public LayerMask Mask => _mask;
    }
}