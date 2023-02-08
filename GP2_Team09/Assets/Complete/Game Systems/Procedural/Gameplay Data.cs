using SS = System.SerializableAttribute;
using GUID = System.Guid;
using UnityEngine;
using GameProject.Interactions;
using System.Collections.Generic;

namespace GameProject.Procedural
{
    /// <summary>
    /// 
    /// </summary>
    [SS] public struct ActionData {
        public string Guid;
        public System.Type Target;
        public InteractionType Action;
    }

    /// <summary>
	/// Contains the simple button object data
	/// </summary>
	[SS] public struct SimpleButtonData {
        public GameObject Prefab;

        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public string Guid;
        public List<List<ActionData>> Actions;
    }

    /// <summary>
	/// Contains the delayed button object data
	/// </summary>
	[SS] public struct DelayedButtonData {
        public GameObject Prefab;

        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public string Guid;
        public float Threshold;
        

    }
}