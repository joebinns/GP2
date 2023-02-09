using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Interactions
{
    [CreateAssetMenu(fileName = "Chaos Settings",
        menuName = "Settings/Chaos Settings")]
    public class ChaosSettings : ScriptableObject
    {
        [Header("Alarm Clock Settings")]
        [SF] private Vector2 _randomTimerRange = new Vector2(20f, 60f);
        [SF] private float _decelerationThreshold = 0.75f;

// PROPERTIES

        public Vector2 RandomTimerRange => _randomTimerRange;
        public float DecelerationThreshold => _decelerationThreshold;
    }
}