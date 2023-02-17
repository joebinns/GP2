using UnityEngine;
using UnityEditor;

namespace Magnuth
{
    public partial class Toolkit : EditorWindow
    {
        private Object replacement = null;

// INITIALIZATION

        [MenuItem("Tools/Magnuth Toolkit")]
        public static void ShowWindow(){
            EditorWindow window = EditorWindow.GetWindow(typeof(Toolkit));
            window.titleContent = new GUIContent("Magnuth Toolkit");
        }

// INTERFACE

        /// <summary>
        /// On draw interface
        /// </summary>
		private void OnGUI(){
            Renaming();
            Sorting();

            Replacing();
        }

        /// <summary>
        /// Starts a new interface category
        /// </summary>
        private void StartCategory(string name){
            GUILayout.Label(name, EditorStyles.boldLabel);
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.Space();

            GUILayout.BeginVertical();
            EditorGUILayout.Space();
        }

        /// <summary>
        /// Ends the last started category
        /// </summary>
        private void EndCategory(){
            EditorGUILayout.Space();
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

    }
}