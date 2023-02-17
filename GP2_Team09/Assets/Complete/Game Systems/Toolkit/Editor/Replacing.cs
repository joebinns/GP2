using UnityEngine;
using UnityEditor;

namespace Magnuth
{
    public partial class Toolkit : EditorWindow
    {
        /// <summary>
        /// Replaces the selected game objects
        /// </summary>
        private void Replacing(){
            StartCategory("REPLACING");

            replacement = EditorGUILayout.ObjectField(
                replacement, typeof(GameObject), false
            );

            if (GUILayout.Button("REPLACE SELECTED"))
                ReplaceSelected();

            EndCategory();
        }

        /// <summary>
        /// Replaces the selected objects with the replacement object
        /// </summary>
        private void ReplaceSelected(){
            if (replacement == null) return;

            var selected = Selection.objects;
            var selection = new Object[selected.Length];

            for (int i = selected.Length - 1; i >= 0; i--){
                var gObj = (GameObject)selected[i];
                if (gObj == null) continue;

                var gTfm = gObj.transform;

                var pObj = (GameObject)PrefabUtility.InstantiatePrefab(
                    replacement, gTfm.parent
                );

                if (pObj == null) continue;
                selection[i] = pObj;

                var pTfm = pObj.transform;
                pTfm.localPosition = gTfm.localPosition;
                pTfm.localRotation = gTfm.localRotation;
                pTfm.localScale = gTfm.localScale;
                pTfm.SetSiblingIndex(gTfm.GetSiblingIndex());

                Undo.RegisterCreatedObjectUndo(pObj, "Replace Selected");
                Undo.DestroyObjectImmediate(gObj);
            }

            Selection.objects = selection;
        }
    }
}