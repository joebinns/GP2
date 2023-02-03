using SF = UnityEngine.SerializeField;
using UnityEngine;
using UnityEngine.UI;

namespace GameProject.HUD
{
    [RequireComponent(typeof(Image))]
    public class Reticle : MonoBehaviour
    {
        [SF] private Image _image;
        [Space]
        [SF] private Sprite _default;
        [SF] private Sprite _pointer;
        [SF] private Sprite _grabOpen;
        [SF] private Sprite _grabClosed;
        
        // TODO: Call SetCursor whenever interactables are hovered, unhovered, pressed and released
        
        public void SetCursor(CursorType cursorType) {
            _image.sprite = cursorType switch {
                CursorType.Default => _default,
                CursorType.Pointer => _pointer,
                CursorType.GrabOpen => _grabOpen,
                CursorType.GrabClosed => _grabClosed,
                _ => _image.sprite
            };
        }
    }
    
    public enum CursorType
    {
        Default,
        Pointer,
        GrabOpen,
        GrabClosed
    }
}
