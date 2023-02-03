using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Levels
{
    [CreateAssetMenu(fileName = "Level Info", menuName = "Settings/Levels")]
    public class LevelInfo : ScriptableObject
    {
        [SF] private string _name = "Level 1";
        [SF] private int _buildIndex = 0;

// PROPERTIES

        public string Name => _name;
        public int BuildIndex => _buildIndex;
    }
}