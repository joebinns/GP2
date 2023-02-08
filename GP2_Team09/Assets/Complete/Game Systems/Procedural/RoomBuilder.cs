using SF = UnityEngine.SerializeField;
using System.Collections;
using UnityEngine;

namespace GameProject.Procedural
{
    [AddComponentMenu("Procedural/Room Builder"), DisallowMultipleComponent]
    public class RoomBuilder : MonoBehaviour
    {
        /// <summary>
        /// Instantiates the room layout in the scene
        /// </summary>
        public IEnumerator Build(Vector3 position, Quaternion rotation){
            //for (int i = 0; i < _layout.Count; i++){
                yield return null;	
			//}
        }
    }
}