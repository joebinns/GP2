#if UNITY_EDITOR
using SF = UnityEngine.SerializeField;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GameProject.Procedural
{
    [AddComponentMenu("Procedural/Room Creator"), DisallowMultipleComponent]
    public class RoomCreator : MonoBehaviour
    {
        [SF] private RoomLayout _layout = null;

        private const string TAG_LAYOUT = "Layout";
        private const string TAG_LIGHT  = "Light";
        private const string TAG_EFFECT = "Effect";
        private const string TAG_REFLECTION = "Reflection";

// LAYOUT CREATION

        /// <summary>
        /// Stores the built room layout
        /// </summary>
        public void AssignToLayout(){
            var layout  = new List<ObjectData>();
            var lights  = new List<LightData>();
            var reflections = new List<ReflectionData>();
            var effects = new List<EffectData>();
            var parent  = this.transform;

            for (int i = 0; i < parent.childCount; i++){
                var group = parent.GetChild(i);

                for (int j = 0; j < group.childCount; j++){
                    var child = group.GetChild(j);

                    switch (child.gameObject.tag){
                        case TAG_LAYOUT: AddToLayout(layout,   child); break;
                        case TAG_LIGHT:  AddToLights(lights,   child); break;
                        case TAG_EFFECT: AddToEffects(effects, child); break;
                        case TAG_REFLECTION: AddToReflections(reflections, child); break;
                    }
                }
            }

            _layout.AssignData(layout, lights, effects, reflections);
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
        /// Adds the effect object to the list of light data
        /// </summary>
		private void AddToEffects(List<EffectData> list, Transform obj){
            var light = obj.GetComponent<Light>();

			var data = new EffectData(){
				Prefab = GetPrefab(obj.gameObject),
				
				Position = obj.localPosition, 
				Rotation = obj.localRotation,
				
                
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
        public void RebuildLayout(){
            var parent = this.transform;

            RebuildEnvironment(parent);
            RebuildLights(parent);
            RebuildReflections(parent);
        }

        /// <summary>
        /// Rebuilds the environment
        /// </summary>
        private void RebuildEnvironment(Transform parent){
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
        private void RebuildLights(Transform parent){
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
        private void RebuildReflections(Transform parent){
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
        /// Instatiates the prefab in the scene
        /// </summary>
        private GameObject SpawnPrefab(GameObject prefab, Transform parent){
            return PrefabUtility.InstantiatePrefab(prefab, parent) as GameObject;
        }
    }
}
#endif