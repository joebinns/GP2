#if UNITY_EDITOR
using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameProject
{
    public class DebugTeleporter : MonoBehaviour
    {
        private readonly Vector3    HubRoomPos = new Vector3(57f, 1.1f, -57.5f);
        private readonly Quaternion HubRoomRot = Quaternion.Euler(0, 0, 0);

        private readonly Vector3    ButtonRoomPos = new Vector3(56.5f, 1.1f, -30f);
        private readonly Quaternion ButtonRoomRot = Quaternion.Euler(0, -90, 0);

        private readonly Vector3    PipeRoomPos = new Vector3(57, 1.1f, -75f);
        private readonly Quaternion PipeRoomRot = Quaternion.Euler(0, 180, 0);

        /// <summary>
        /// Teleports player to position
        /// </summary>
        private void Update(){
            var keyboard = Keyboard.current;

            if (keyboard[Key.F9].isPressed){
                transform.position = HubRoomPos;
                transform.rotation = HubRoomRot;
            
            } else if (keyboard[Key.F10].isPressed){
                transform.position = ButtonRoomPos;
                transform.rotation = ButtonRoomRot;
            
            } else if (keyboard[Key.F11].isPressed){
                transform.position = PipeRoomPos;
                transform.rotation = PipeRoomRot;
            }
        }
    }
}
#endif