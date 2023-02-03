using UnityEngine;

namespace GameProject.Environment
{
    public class Door : MonoBehaviour
    {
        public void OpenDoor() {
            gameObject.SetActive(false);
        }
    }
}
