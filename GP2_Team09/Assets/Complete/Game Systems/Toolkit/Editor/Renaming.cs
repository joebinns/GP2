using UnityEngine;
using UnityEditor;

namespace Magnuth
{
    public partial class Toolkit : EditorWindow
    {
        /// <summary>
        /// Renames the selected game objects
        /// </summary>
        private void Renaming(){
            StartCategory("RENAMING");

            if (GUILayout.Button("REMOVE GAMEOBJECT NUMBER"))
                RemoveNumber();

            EditorGUILayout.Space();

            if (GUILayout.Button("RENAME GAMEOBJECT TO MESH"))
                SetToMeshName(false);

            if (GUILayout.Button("RENAME PREFAB TO MESH"))
                SetToMeshName(true);

            EndCategory();
        }

        /// <summary>
        /// Renames the selected game objects, removing the copy number
        /// </summary>
        private void RemoveNumber(){
            var selected = Selection.objects;

            for (int i = 0; i < selected.Length; i++){
                var obj = (GameObject)selected[i];
                if (obj == null) continue;

                var name = obj.name;
                var index = name.LastIndexOf('(');
                if (index < 0) continue;

                name = name.Remove(index, name.Length - index);
                selected[i].name = name;
            }
        }

        /// <summary>
        /// Renames the selected game objects to be the same as their mesh name
        /// </summary>
        private void SetToMeshName(bool asset){
            var selected = Selection.objects;

            for (int i = 0; i < selected.Length; i++){
                var obj = (GameObject)selected[i];
                if (obj == null) continue;

                var filter = obj.GetComponent<MeshFilter>();
                if (filter == null || filter.sharedMesh == null) continue;

                if (asset){
                    var path = AssetDatabase.GetAssetPath(obj);
                    AssetDatabase.RenameAsset(path, filter.sharedMesh.name);

                } else Selection.objects[i].name = filter.sharedMesh.name;
            }
        }
    }
}
