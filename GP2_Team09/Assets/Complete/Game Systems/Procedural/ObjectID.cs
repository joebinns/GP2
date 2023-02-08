using SF = UnityEngine.SerializeField;
using GUID = System.Guid;
using UnityEngine;

namespace GameProject.Interactions
{
    public class ObjectID : MonoBehaviour
    {
        [SF] private string _guid = null;

        public string Guid {
            get { 
                if (_guid == null)
                    _guid = GUID.NewGuid().ToString();

                return _guid;
            }
            set => _guid = value;
        }
    }
}