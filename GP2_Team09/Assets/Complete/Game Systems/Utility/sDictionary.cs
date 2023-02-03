using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameProject
{
    /// <summary>
    /// Serializable Dictionary
    /// </summary>
    /// <remarks>Based on the version created by christophfranke123 and Zoodinger</remarks>
    /// <see cref="https://answers.unity.com/questions/460727/how-to-serialize-dictionary-with-unity-serializati.html"/>
    [System.Serializable]
    public class sDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SF] private List<TKey> _keys = new List<TKey>();
        [SF] private List<TValue> _values = new List<TValue>();

        /// <summary>
        /// Add to dictionary
        /// </summary>
        public new void Add(TKey key, TValue value){
            if (TryAdd(key, value)){
                _keys.Add(key);
                _values.Add(value);
            }
        }

        /// <summary>
        /// Saves dictionary to lists
        /// </summary>
        public void OnBeforeSerialize(){
            _keys.Clear();
            _values.Clear();

            if (typeof(TKey) == typeof(Object) ||
                typeof(TKey).IsSubclassOf(typeof(Object))){

                foreach (var element in this.Where(element => element.Key != null)){
                    _keys.Add(element.Key);
                    _values.Add(element.Value);
                }

            } else {
                foreach (var element in this){
                    _keys.Add(element.Key);
                    _values.Add(element.Value);
                }
            }
        }

        /// <summary>
        /// Loads dictionary from lists
        /// </summary>
        public void OnAfterDeserialize(){
            Clear();

            if (typeof(TKey) == typeof(Object) ||
                typeof(TKey).IsSubclassOf(typeof(Object))){

                for (int i = 0; i < _keys.Count; i++){
                    var key = _keys[i];

                    if (key != null)
                        Add(key, _values[i]);
                }

            } else {
                for (int i = 0; i < _keys.Count; i++){
                    Add(_keys[i], _values[i]);
                }
            }
        }
    }
}