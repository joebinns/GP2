using UnityEngine;

namespace GameProject.Interactions 
{
    [System.SerializableAttribute]
    public class PromptInfo
    {
        public Sprite DesktopIcon;
        public Sprite ConsoleIcon;
        public string Verb;

        /// <summary>
        /// Check if the prompt contains any information
        /// </summary>
        public bool IsNull() => DesktopIcon == null && ConsoleIcon == null && Verb == "";
    }
}