using UnityEngine;
using UnityEditor;

namespace GameProject.Procedural
{
    [CustomEditor(typeof(RoomCreator))]
    public class RoomCreatorEditor : Editor
    {
        private RoomCreator _target = null;

        /// <summary>
        /// Initialises the editor
        /// </summary>
        private void OnEnable(){
            _target = (RoomCreator)target;
        }

        /// <summary>
        /// Draws the editor inspector
        /// </summary>
        public override void OnInspectorGUI(){
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            if (GUILayout.Button("Assign Layout"))
                _target.AssignToLayout();

            if (GUILayout.Button("Rebuild Layout"))
                _target.BuildLayout();
        }
    }
}