using SS = System.SerializableAttribute;
using UnityEngine;

namespace GameProject.Procedural
{
	/// <summary>
	/// Contains the environment object data
	/// </summary>
    [SS] public struct ObjectData {
        public GameObject Prefab;
		
        public Vector3[] Position;
        public Quaternion[] Rotation;
        public Vector3[] Scale;
    }
	
	/// <summary>
	/// Contains the light object data
	/// </summary>
	[SS] public struct LightData {
        public GameObject Prefab;
		
        public Vector3 Position;
        public Quaternion Rotation;

		public float Range;
        public float Angle;
        public float Intensity;
		public Color Colour;
    }

    /// <summary>
	/// Contains the light object data
	/// </summary>
	[SS] public struct EffectData {
        public GameObject Prefab;
		
        public Vector3 Position;
        public Quaternion Rotation;

		
    }

    /// <summary>
    /// Contains the reflection probe object data
    /// </summary>
    [SS] public struct ReflectionData {
        public GameObject Prefab;

        public Vector3 Position;
        public Quaternion Rotation;

        public Vector3 Size;
        public Vector3 Center;
    }
}