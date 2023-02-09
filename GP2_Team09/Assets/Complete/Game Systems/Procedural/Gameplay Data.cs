using SS = System.SerializableAttribute;
using GUID = System.Guid;
using UnityEngine;
using GameProject.Interactions;
using System.Collections.Generic;

namespace GameProject.Procedural
{
    /// <summary>
    /// Contains the interaction action info data
    /// </summary>
    [SS] public struct ActionData {
        public string Guid;
        public int Script;
        public InteractionType Action;
    }

    /// <summary>
	/// Contains the interaction object data
	/// </summary>
	[SS] public struct InteractionData {
        public GameObject Prefab;

        public Vector3[] Position;
        public Quaternion[] Rotation;
        public Vector3[] Scale;

        public string Guid;
        public List<List<List<ActionData>>> Actions;
    }
}