using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    [CreateAssetMenu(fileName = "Chaos Settings",
        menuName = "Settings/Chaos Settings")]
    public class ChaosSettings : ScriptableObject
    {
        [Header("Teleport System Settings")]
        [SF] private float _maxUnsafeDuration = 10f;
        [SF] private float _maxUnsafeDistance = 40f;
        
        [Header("Alarm Clock Settings")]
        [SF] private Vector2 _randomTimerRange = new Vector2(20f, 60f);
        [SF] private float _decelerationThreshold = 0.75f;

        [Header("Ceiling Light Settings")]
        [SF] private Vector2 _randomLightTimer = new Vector2(20f, 60f);

// PROPERTIES

        public float MaxUnsafeDuration => _maxUnsafeDuration;
        public float MaxUnsafeDistance => _maxUnsafeDistance;

        public Vector2 RandomTimerRange => _randomTimerRange;
        public float DecelerationThreshold => _decelerationThreshold;

        public Vector2 RandomLightTimer => _randomLightTimer;
    }
}