using UnityEditor;
using UnityEngine;

namespace GameProject.Interactions
{
    [CustomPropertyDrawer(typeof(ActionInfo))]
    public class ActionInfoDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label){
            var target = property.FindPropertyRelative("Target");
            var action = property.FindPropertyRelative("Action");

            position.width = position.width * 0.52f - 10;
            EditorGUI.PropertyField(position, target, GUIContent.none);

            position.x += position.width + 9;
            EditorGUI.PropertyField(position, action, GUIContent.none);
        }
    }
}