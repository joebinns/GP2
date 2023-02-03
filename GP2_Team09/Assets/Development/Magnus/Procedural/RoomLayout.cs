using SS = System.SerializableAttribute;
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameProject.ProcGen
{
    [CreateAssetMenu(fileName = "Room Layout", menuName = "Procedural/Room Layout")]
    public class RoomLayout : ScriptableObject
    {
        [SF] private List<Piece> _layout = null;

// PROPERTIES

        public List<Piece> Layout => _layout;

 // LAYOUT HANDLING

        /// <summary>
        /// Instantiates the room layout in the scene
        /// </summary>
        public void Build(Vector3 position, Quaternion rotation){
            
        }

#if UNITY_EDITOR
        
        /// <summary>
        /// Stores the manually built room layout
        /// </summary>
        public void Assign(Transform layout){
            _layout.Clear();

            for (int i = 0; i < layout.childCount; i++){
                var obj = layout.GetChild(i);
                var prefab = GetPrefab(obj.gameObject);

                _layout.Add(new Piece(){ 
                    Prefab = prefab, 
                    Position = obj.localPosition, 
                    Rotation = obj.localRotation, 
                    Scale = obj.localScale
                });
            }
        }

        /// <summary>
        /// Returns the prefab asset from game object instance
        /// </summary>
        private GameObject GetPrefab(GameObject instance){
            var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(instance);
            return (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        }

#endif
    }
}