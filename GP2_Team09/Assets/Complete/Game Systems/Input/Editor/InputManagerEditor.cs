using UnityEditor;

namespace GameProject.Inputs 
{
    [CustomEditor(typeof(InputManager))]
    public class InputManagerEditor : Editor {
        // This is a hack that enables the InputInfo
        // property drawer to use EditorGUILayout
    }
}