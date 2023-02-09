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

// STORING

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
		private void AddToLayout(List<ObjectData> list, Transform parent){
            GetTransform(parent, out var positions, out var rotations, out var scales);

            var data = new ObjectData(){
				Prefab = GetPrefab(parent.gameObject),
				
				Position = positions, 
				Rotation = rotations, 
				Scale    = scales
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
        /// Returns the prefab asset from game object instance
        /// </summary>
        private GameObject GetPrefab(GameObject instance){
            var path = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(instance);
            return (GameObject)AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        }

// REBUILDING

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
        private void RebuildEnvironment(Transform room){
            var layout = _layout.Layout;
            
            for (int i = 0; i < layout.Count; i++){
                var data   = layout[i];
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