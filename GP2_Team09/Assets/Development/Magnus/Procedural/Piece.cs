using SS = System.SerializableAttribute;
using UnityEngine;

namespace GameProject.ProcGen
{
    [SS] public struct Piece {
        public GameObject Prefab;

        public Vector3 Position, Scale;
        public Quaternion Rotation;
    }
}