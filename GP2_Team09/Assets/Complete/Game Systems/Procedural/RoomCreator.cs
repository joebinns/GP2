#if UNITY_EDITOR
using SF = UnityEngine.SerializeField;
using GUID = System.Guid;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameProject.Interactions;

namespace GameProject.Procedural
{
    [AddComponentMenu("Procedural/Room Creator"), DisallowMultipleComponent]
    public class RoomCreator : MonoBehaviour
    {
        [SF] private RoomLayout _layout = null;
        private Dictionary<GUID, GameObject> _gameplay = null;

        private const string TAG_LAYOUT = "Layout";
        private const string TAG_LIGHT  = "Light";
        private const string TAG_REFLECTION = "Reflection";
        private const string TAG_BUTTON = "Button";

// LAYOUT CREATION

        /// <summary>
        /// Stores the built room layout
        /// </summary>
        public void AssignToLayout(){
            var layout  = new List<ObjectData>();
            var lights  = new List<LightData>();
            var reflections = new List<ReflectionData>();
            var buttons = new List<SimpleButtonData>();
            var parent  = this.transform;

            for (int i = 0; i < parent.childCount; i++){
                var child = parent.GetChild(i);

                switch (child.gameObject.tag){
                    case TAG_LAYOUT: AddToLayout(layout,  child); break;
                    case TAG_LIGHT:  AddToLights(lights,  child); break;
                    case TAG_REFLECTION: AddToReflections(reflections, child); break;
                    case TAG_BUTTON: AddToButton(buttons, child); break;
                }
            }

            _layout.AssignData(layout, lights, reflections, buttons);
            EditorUtility.SetDirty(_layout);
        }
		
        /// <summary>
        /// Adds the environment object to the list of layout data
        /// </summary>
		private void AddToLayout(List<ObjectData> list, Transform obj){
			var data = new ObjectData(){
				Prefab = GetPrefab(obj.gameObject),
				
				Position = obj.localPosition, 
				Rotation = obj.localRotation, 
				Scale    = obj.localScale
			};
            list.Add(data);
		}

        /// <summary>
        /// Adds the light object to the list of light data
        /// </summary>
		private void AddToLights(List<LightData> list, Transform obj){
            var light = obj.GetComponent<Light>();

			var data = new LightData(){
				Prefab = GetPrefab(obj.gameObject),
				
				Position = obj.localPosition, 
				Rotation = obj.localRotation,
				
                Range     = light.range,
                Angle     = light.spotAngle,
                Intensity = light.intensity,
                Colour    = light.color,
            };
            list.Add(data);
		}

        /// <summary>
        /// Adds the reflection probe object to the list of reflection data
        /// </summary>
		private void AddToReflections(List<ReflectionData> list, Transform obj){
            var probe = obj.GetComponent<ReflectionProbe>();

            var data = new ReflectionData(){
                Prefab = GetPrefab(obj.gameObject),

                Position = obj.localPosition,
                Rotation = obj.localRotation,

                Size   = probe.size,
                Center = probe.center,
            };
            list.Add(data);
		}

        /// <summary>
        /// Adds the button object to the list of button data
        /// </summary>
        private void AddToButton(List<SimpleButtonData> list, Transform obj){
            var interactions = obj.GetComponents<BaseInteraction>();

            for (int i = 0; i < interactions.Length; i++){
                var action = interactions[i];

                var data = new SimpleButtonData(){
                    Prefab = GetPrefab(obj.gameObject),

                    Position = obj.localPosition,
                    Rotation = obj.localRotation,

                    // IDEA 1: Cast action to correct script from System.Type
                    // Get all action lists from script (hard code on a script to script basis)

                    // IDEA 2: Have an abstract List<List<ActionInfo> GetActions method in BaseInteraction
                    // Each script has to define the return value

                    // IDEA 3: Cast action to correct script from System.Type
                    // Use reflection to get the lists in each script

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
        /// Rebuilds the layout in the scene (edit mode only)
        /// </summary>
        public void BuildLayout(){
            var parent = this.transform;

            BuildEnvironment(parent);
            BuildLights(parent);
            BuildReflections(parent);
            BuildButtons(parent);

            //ConnectButtons();
        }

        /// <summary>
        /// Rebuilds the environment
        /// </summary>
        private void BuildEnvironment(Transform parent){
            var layout = _layout.Layout;
            
            for (int i = 0; i < layout.Count; i++){
                var data  = layout[i];
                var child = SpawnPrefab(data.Prefab, parent);

                var tfm = child.transform;
                tfm.localPosition = data.Position;
                tfm.localRotation = data.Rotation;
                tfm.localScale    = data.Scale;
            }
        }

        /// <summary>
        /// Rebuilds the lights
        /// </summary>
        private void BuildLights(Transform parent){
            var lights = _layout.Lights;
            
            for (int i = 0; i < lights.Count; i++){
                var data  = lights[i];
                var child = SpawnPrefab(data.Prefab, parent);

                var tfm = child.transform;
                tfm.localPosition = data.Position;
                tfm.localRotation = data.Rotation;
                
                var light = child.GetComponent<Light>();
                light.range     = data.Range;
                light.spotAngle = data.Angle;
                light.intensity = data.Intensity;
                light.color     = data.Colour;
            }
        }

        /// <summary>
        /// Rebuilds the reflection probes
        /// </summary>
        private void BuildReflections(Transform parent){
            var lights = _layout.Reflections;
            
            for (int i = 0; i < lights.Count; i++){
                var data  = lights[i];
                var child = SpawnPrefab(data.Prefab, parent);

                var tfm = child.transform;
                tfm.localPosition = data.Position;
                tfm.localRotation = data.Rotation;
                
                var probe = child.GetComponent<ReflectionProbe>();
                probe.size = data.Size;
                probe.center = data.Center;
            }
        }

        /// <summary>
        /// Rebuilds the buttons
        /// </summary>
        private void BuildButtons(Transform parent){
            _gameplay = new Dictionary<GUID, GameObject>();
            var buttons = _layout.Buttons;

            for (int i = 0; i < buttons.Count; i++){
                var data  = buttons[i];
                var child = SpawnPrefab(data.Prefab, parent);

                var tfm = child.transform;
                tfm.localPosition = data.Position;
                tfm.localRotation = data.Rotation;
                tfm.localScale    = data.Scale;

                // Assign stored guid back to obj

                _gameplay.Add(data.Guid, child);
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
        /// 
        /// </summary>
        private void ConnectButtons(){
            var buttons = _layout.Buttons;

            for (int i = 0; i < buttons.Count; i++){
                var data  = buttons[i];
                var child = _gameplay[data.Guid];

                //var button = child.GetComponent<ButtonInteraction>();

            }
        }
    }
}
#endif