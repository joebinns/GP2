using SS = System.SerializableAttribute;
using UnityEngine;

namespace GameProject.Interactions 
{
    [SS] public class PromptInfo
    {
        public Sprite DesktopIcon;
        public Sprite ConsoleIcon;
        public string Verb;

        public bool IsNull() => DesktopIcon == null && ConsoleIcon == null && Verb == "";
    }
}