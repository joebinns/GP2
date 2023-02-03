using UnityEditor;
using UnityEngine;

namespace GameProject.Inputs
{
    [CustomPropertyDrawer(typeof(InputInfo))]
    public class InputInfoDrawer : PropertyDrawer
    {
        private readonly float Spacing = 80;
        private readonly float BoxPadding = 7.5f;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label){
            var input    = property.FindPropertyRelative("Input");
            var pressed  = property.FindPropertyRelative("Pressed");
            var changed  = property.FindPropertyRelative("Changed");
            var released = property.FindPropertyRelative("Released");
            var sorted   = property.FindPropertyRelative("Sorted");

            // References
            var lineHeight = EditorGUIUtility.singleLineHeight;
            var defWidth = EditorGUIUtility.labelWidth;

            // Background box
            rect.width += BoxPadding;
            rect.x -= BoxPadding * 0.5f;
            rect.y -= BoxPadding * 0.25f - 1;
            rect.height += lineHeight * 2.5f + (BoxPadding * 0.5f);
            GUI.Box(rect, "", EditorStyles.helpBox);
            
            // Fix spacing
            GUILayout.Space(-EditorGUIUtility.singleLineHeight);
            EditorGUIUtility.labelWidth = Spacing;

            // Draw input
            EditorGUILayout.PropertyField(input, label);

            // Draw event check boxes
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(pressed);
                GUILayout.FlexibleSpace();

                EditorGUILayout.PropertyField(changed);
                GUILayout.FlexibleSpace();

                EditorGUILayout.PropertyField(released);
            EditorGUILayout.EndHorizontal();

            // Draw sorted checkbox
            EditorGUILayout.PropertyField(sorted);

            // Fix spacing
            EditorGUIUtility.labelWidth = defWidth;
            GUILayout.Space(EditorGUIUtility.singleLineHeight);
        }
    }
}