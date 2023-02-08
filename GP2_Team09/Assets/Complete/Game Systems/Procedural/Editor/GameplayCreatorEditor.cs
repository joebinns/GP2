using UnityEngine;
using UnityEditor;

namespace GameProject.Procedural
{
    [CustomEditor(typeof(GameplayCreator))]
    public class GameplayCreatorEditor : Editor
    {
        private GameplayCreator _target = null;

        /// <summary>
        /// Initialises the editor
        /// </summary>
        private void OnEnable(){
            _target = (GameplayCreator)target;
        }

        /// <summary>
        /// Draws the editor inspector
        /// </summary>
        public override void OnInspectorGUI(){
            base.OnInspectorGUI();
            EditorGUILayout.Space();

            if (GUILayout.Button("Assign Gameplay"))
                _target.AssignToGameplay();

            if (GUILayout.Button("Rebuild Gameplay"))
                _target.RebuildGameplay();
        }
    }
}