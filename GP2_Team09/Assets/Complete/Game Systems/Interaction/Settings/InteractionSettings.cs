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

        [Header("Hold Settings")]
        [SF] private bool _autoOrientOnGrab = false;
        [SF] private bool _toggleGravity = true;

        [Header("Hold Rotation Settings")]
        [SF] private Vector2 _axes = Vector2.one;
        
        [Header("Hold Distance Settings")]
        [SF][Range(0f, 1f)] private float _holdDistanceSensitivity = 0.5f;
        [SF] private Vector2 _holdDistanceRange = new Vector2(2f, 5f);

        private const float HOLD_DIST_SENS_MULT = 120f;

// PROPERTIES

        public float Radius => _radius;
        public float Distance => _distance;
        public LayerMask Mask => _mask;
        public Vector2 Axes => _axes;
        public float HoldDistanceSensitivity => _holdDistanceSensitivity * HOLD_DIST_SENS_MULT;
        public Vector2 HoldDistanceRange => _holdDistanceRange;
        public bool AutoOrientOnGrab => _autoOrientOnGrab;
        public bool ToggleGravity => _toggleGravity;
    }
}