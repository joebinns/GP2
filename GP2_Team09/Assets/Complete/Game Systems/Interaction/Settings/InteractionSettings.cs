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
        [SF] private float _defaultHoldDistance = 3f;
        [SF][Range(0f, 1f)] private float _holdDistanceSensitivity = 0.5f;
        [SF] private Vector2 _holdDistanceRange = new Vector2(2f, 5f);
        [SF] private bool _autoOrientOnGrab = false;

        private const float HOLD_DIST_SENS_MULT = 30f;

// PROPERTIES

        public float Radius => _radius;
        public float Distance => _distance;
        public LayerMask Mask => _mask;

        public float DefaultHoldDistance => _defaultHoldDistance;
        public float HoldDistanceSensitivity => _holdDistanceSensitivity * HOLD_DIST_SENS_MULT;
        public Vector2 HoldDistanceRange => _holdDistanceRange;
        public bool AutoOrientOnGrab => _autoOrientOnGrab;
    }
}