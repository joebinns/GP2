using SF = UnityEngine.SerializeField;
using UnityEngine;

namespace GameProject.Game
{
    [CreateAssetMenu(fileName = "Ship Names", menuName = "Settings/Ship Names")]
    public class ShipNames : ScriptableObject 
    {
        [SF] private string[] _names = null;

        public string[] Names => _names;
    }
}