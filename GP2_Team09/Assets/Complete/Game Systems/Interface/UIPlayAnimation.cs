using UnityEngine.EventSystems;
using UnityEngine;

namespace GameProject.Interface
{
    public class UIPlayAnimation : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerEnter(PointerEventData eventData){
            Debug.Log("Entered");
        }

        public void OnPointerExit(PointerEventData eventData){
            if (!eventData.fullyExited) return;
            Debug.Log("Exited");
        }

        public void OnPointerDown(PointerEventData eventData){
            Debug.Log("Clicked");
        }

        public void OnPointerUp(PointerEventData eventData){
            Debug.Log("Released");
        }
    }
}