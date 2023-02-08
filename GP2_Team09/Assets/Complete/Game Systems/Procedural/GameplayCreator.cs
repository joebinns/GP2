#if UNITY_EDITOR
using SF = UnityEngine.SerializeField;
using GUID = System.Guid;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameProject.Interactions;

namespace GameProject.Procedural
{
    [AddComponentMenu("Procedural/Gameplay Creator"), DisallowMultipleComponent]
    public class GameplayCreator : MonoBehaviour
    {
        [SF] private GameplaySetup _gameplay = null;
        private Dictionary<string, GameObject> _interactables = null;

        private const string TAG_GAMEPLAY = "Gameplay";

// LAYOUT CREATION

        /// <summary>
        /// Stores the built gameplay setup
        /// </summary>
        public void AssignToGameplay(){
            var buttons = new List<SimpleButtonData>();
            var parent  = this.transform;

            for (int i = 0; i < parent.childCount; i++){
                var group = parent.GetChild(i);

                for (int j = 0; j < group.childCount; j++){
                    var child = group.GetChild(j);

                    switch (child.gameObject.tag){
                        case TAG_GAMEPLAY: AddToInteractables(buttons, child); break;
                    }
                }
            }

            _gameplay.AssignData(buttons);
            EditorUtility.SetDirty(_gameplay);
        }

        /// <summary>
        /// Adds the button object to the list of button data
        /// </summary>
        private void AddToInteractables(List<SimpleButtonData> list, Transform obj){
            var actions = obj.GetComponents<BaseInteraction>();
            var guid    = obj.GetComponent<ObjectID>();

            for (int i = 0; i < actions.Length; i++){
                var action = actions[i];

                var data = new SimpleButtonData() {
                    Prefab = GetPrefab(obj.gameObject),

                    Position = obj.localPosition,
                    Rotation = obj.localRotation,

                    //Guid = guid.Guid,

                    // Convert all ActionInfo's into ActionData
                };

                list.Add(data);
            }
        }
		
        /// <summary>
        /// Returns the prefab asset from game object instance
        /// </summary>
        private GameObject GetPrefab(GameObject instance){
            var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(instance);
            return (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        }

// LAYOUT BUILDING

        /// <summary>
        /// Rebuilds the gameplay in the scene (edit mode only)
        /// </summary>
        public void RebuildGameplay(){
            var parent = this.transform;

            RebuildInteractable(parent);
            ReconnectInteractables();
        }

        /// <summary>
        /// Rebuilds the interactables
        /// </summary>
        private void RebuildInteractable(Transform parent){
            _interactables = new Dictionary<string, GameObject>();
            var buttons = _gameplay.Buttons;

            for (int i = 0; i < buttons.Count; i++){
                var data  = buttons[i];
                var child = SpawnPrefab(data.Prefab, parent);

                var tfm = child.transform;
                tfm.localPosition = data.Position;
                tfm.localRotation = data.Rotation;
                tfm.localScale    = data.Scale;

                // Assign stored guid back to obj

                _interactables.Add(data.Guid, child);
            }
        }

        /// <summary>
        /// Instatiates the prefab in the scene
        /// </summary>
        private GameObject SpawnPrefab(GameObject prefab, Transform parent){
            return PrefabUtility.InstantiatePrefab(prefab, parent) as GameObject;
        }

// REBUILD GAMEPLAY

        /// <summary>
        /// Rebuilds the interactable connections
        /// </summary>
        private void ReconnectInteractables(){
            var buttons = _gameplay.Buttons;

            for (int i = 0; i < buttons.Count; i++){
                var data  = buttons[i];

                var child = _interactables[data.Guid];
                //var button = child.GetComponent<ButtonInteraction>();

            }
        }
    }
}
#endif