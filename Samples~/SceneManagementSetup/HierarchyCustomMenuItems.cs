using System.IO;
using UnityEditor;
using UnityEngine;

namespace FinishOne.SceneManagement
{
    public class HierarchyCustomMenuItems
    {
        private static string packagePath = "Packages/com.finishone.scene-management";
        private static string relativePath = "Runtime/Prefabs/";


        [MenuItem("GameObject/SceneManagement/SceneLoadRequester", false, 10)]
        private static void CreateSceneLoadRequester(MenuCommand command)
        {
            string prefabName = "SceneLoadRequester.prefab";

            SpawnPrefab(prefabName, command);
        }


        [MenuItem("GameObject/SceneManagement/Initializer", false, 10)]
        private static void CreateInitializer(MenuCommand command)
        {
            string prefabName = "Initializer.prefab";

            SpawnPrefab(prefabName, command);
        }



        private static void SpawnPrefab(string prefabName, MenuCommand command)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(Path.Combine(packagePath, relativePath, prefabName));

            GameObject spawnedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

            GameObjectUtility.SetParentAndAlign(spawnedPrefab, command.context as GameObject);
            Undo.RegisterCreatedObjectUndo(spawnedPrefab, "Create " + spawnedPrefab.name);
            Selection.activeObject = spawnedPrefab;
        }
    }
}