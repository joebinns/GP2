#if UNITY_EDITOR
using SF = UnityEngine.SerializeField;
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

// STORING

        /// <summary>
        /// Stores the built gameplay setup
        /// </summary>
        public void AssignToGameplay() {
            var buttons = new List<InteractionData>();
            var parent = this.transform;

            for (int i = 0; i < parent.childCount; i++) {
                var group = parent.GetChild(i);

                for (int j = 0; j < group.childCount; j++) {
                    var child = group.GetChild(j);

                    switch (child.gameObject.tag) {
                        case TAG_GAMEPLAY: AddToInteractables(buttons, child); break;
                    }
                }
            }

            _gameplay.AssignData(buttons);
            EditorUtility.SetDirty(_gameplay);
        }

        /// <summary>
        /// Adds the interactable object to the list of interactables
        /// </summary>
        private void AddToInteractables(List<InteractionData> list, Transform group) {
            var guid = group.GetComponent<ObjectID>();

            var interactions = group.GetComponents<BaseInteraction>();
            var actions = GetActions(interactions);

            GetTransform(group, out var positions, out var rotations, out var scales);

            var data = new InteractionData(){
                Prefab = GetPrefab(group.gameObject),

                Position = positions,
                Rotation = rotations,
                Scale = scales,

                Guid = guid.Guid,
                Actions = actions,
            };

            list.Add(data);
        }

        /// <summary>
        /// Outputs the object parent and children transform
        /// </summary>
        private void GetTransform(Transform parent, out Vector3[] positions, out Quaternion[] rotations, out Vector3[] scales){
            var children = parent.childCount;

            positions = new Vector3[children + 1];
            rotations = new Quaternion[children + 1];
            scales    = new Vector3[children + 1];

            positions[0] = parent.localPosition;
            rotations[0] = parent.localRotation;
            scales[0]    = parent.localScale;

            for (int i = 1; i <= children; i++){
                var child = parent.GetChild(i - 1);

                positions[i] = child.localPosition;
                rotations[i] = child.localRotation;
                scales[i]    = child.localScale;
            }
        }

        /// <summary>
        /// Returns a list of all interaction events and actions
        /// </summary>
        private List<List<List<ActionData>>> GetActions(BaseInteraction[] interactions){
            var actions = new List<List<List<ActionData>>>();

            for (int i = 0; i < interactions.Length; i++){
                actions.Add(GetEvents(interactions[i]));
            }

            return actions;
        }

        /// <summary>
        /// Returns the list of events with list of actions
        /// </summary>
        private List<List<ActionData>> GetEvents(BaseInteraction interaction){
            var list   = new List<List<ActionData>>();
            var events = interaction.GetActions();

            for (int i = 0; i < events.Count; i++)
                list.Add(GetActions(events[i]));

            return list;
        }

        /// <summary>
        /// Returns the list of actions
        /// </summary>
        private List<ActionData> GetActions(List<ActionInfo> actions){
            var list = new List<ActionData>();

            for (int i = 0; i < actions.Count; i++){
                var action = actions[i];
                var target = action.Target;
                var guid   = target.GetComponent<ObjectID>();

                var bint  = target.GetComponents<BaseInteraction>();
                var index = System.Array.IndexOf(bint, target);

                list.Add(new ActionData(){
                    Guid   = guid.Guid,
                    Script = index,
                    Action = action.Action,
                });
            }

            return list;
        }
		
        /// <summary>
        /// Returns the prefab asset from game object instance
        /// </summary>
        private GameObject GetPrefab(GameObject instance){
            var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(instance);
            return (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        }

// REBUILDING

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
        private void RebuildInteractable(Transform room){
            _interactables   = new Dictionary<string, GameObject>();
            var interactions = _gameplay.Interactables;

            for (int i = 0; i < interactions.Count; i++){
                var data  = interactions[i];
                var parent = SpawnPrefab(data.Prefab, room);

                var tfm = parent.transform;
                tfm.localPosition = data.Position[0];
                tfm.localRotation = data.Rotation[0];
                tfm.localScale    = data.Scale[0];

                for (int j = 0; j < tfm.childCount; j++){
                    var child = tfm.GetChild(j);

                    child.localPosition = data.Position[j + 1];
                    child.localRotation = data.Rotation[j + 1];
                    child.localScale    = data.Scale[j + 1];
                }

                var ID  = parent.GetComponent<ObjectID>();
                ID.Guid = data.Guid;

                _interactables.Add(data.Guid, parent);
            }
        }

        /// <summary>
        /// Instatiates the prefab in the scene
        /// </summary>
        private GameObject SpawnPrefab(GameObject prefab, Transform parent){
            return PrefabUtility.InstantiatePrefab(prefab, parent) as GameObject;
        }

// RECONNECTING

        /// <summary>
        /// Rebuilds the interactable connections
        /// </summary>
        private void ReconnectInteractables(){
            var interactables = _gameplay.Interactables;

            for (int i = 0; i < interactables.Count; i++){
                SetInteractions(interactables[i]);
            }
        }

        /// <summary>
        /// Reassigns all of the actions to the interactable
        /// </summary>
        private void SetInteractions(InteractionData interactable){
            var list = interactable.Actions;

            var child = _interactables[interactable.Guid];
            var interactions = child.GetComponents<BaseInteraction>();

            for (int i = 0; i < list.Count; i++){
                var interaction = interactions[i];
                
                var events = GetInfos(list[i]);
                interaction.SetActions(events);
            }
        }

        /// <summary>
        /// Returns the list of events with list of action infos
        /// </summary>
        private List<List<ActionInfo>> GetInfos(List<List<ActionData>> events){
            var list = new List<List<ActionInfo>>();

            for (int i = 0; i < events.Count; i++){
                var infos   = new List<ActionInfo>();
                var actions = events[i];

                for (int j = 0; j < actions.Count; j++){
                    var action = actions[j];
                            
                    var target = _interactables[action.Guid];
                    var bint   = target.GetComponents<BaseInteraction>();

                    infos.Add(new ActionInfo(){
                        Target = bint[action.Script],
                        Action = action.Action,
                    });
                }

                list.Add(infos);
            }

            return list;
        }
    }
}
#endif