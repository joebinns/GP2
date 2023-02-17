using UnityEngine;
using UnityEditor;

using System.Collections.Generic;
using System.Linq;

namespace Magnuth
{
    public partial class Toolkit : EditorWindow
    {
        /// <summary>
        /// Sorts the selected game objects
        /// </summary>
        private void Sorting(){
            StartCategory("SORTING");

            if (GUILayout.Button("SORT CHILDREN BY ZXY"))
                SortChildren(true, false);

            if (GUILayout.Button("SORT CHILDREN BY XZY"))
                SortChildren(false, true);

            EndCategory();
        }

        /// <summary>
        /// Sorts the children by local position
        /// </summary>
        private void SortChildren(bool zxy, bool xzy){
            foreach (GameObject selected in Selection.objects){
                var children = new List<Transform>(
                    selected.GetComponentsInChildren<Transform>()
                );

                if (zxy) children = SortZXY(children);
                if (xzy) children = SortXZY(children);
                Undo.RecordObjects(children.ToArray(), "Sort Children");

                for (int i = 0; i < children.Count; i++){
                    var child = children[i];

                    child.SetSiblingIndex(
                        children.IndexOf(child)
                    );
                }
            }
        }

        /// <summary>
        /// Sorts the children by local Z,X,Y position
        /// </summary>
        private List<Transform> SortZXY(List<Transform> list){
            for (int i = 0; i < 5; i++){
                list = list.OrderBy(p => p.localPosition.y)
                           .ThenBy(p => p.localPosition.x)
                           .ThenBy(p => p.localPosition.z)
                           .ToList();
            }
            return list;
        }

        /// <summary>
        /// Sorts the children by local X,Z,Y position
        /// </summary>
        private List<Transform> SortXZY(List<Transform> list){
            for (int i = 0; i < 5; i++){
                list = list.OrderBy(p => p.localPosition.y)
                           .ThenBy(p => p.localPosition.z)
                           .ThenBy(p => p.localPosition.x)
                           .ToList();
            }
            return list;
        }
    }
}
