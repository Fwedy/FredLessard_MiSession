using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class EditorMenu : MonoBehaviour
{

    

    [MenuItem("Tools/CountSceneNumber")]

    static void CountSceneNumber()
    {
        Debug.Log(GetSceneAssets().Count);
    }

    static string[] searchInScenes = new[] { "Assets/Scenes/" };
    static List<SceneAsset> GetSceneAssets()
    {

        string[] sceneGuids = AssetDatabase.FindAssets("t:SceneAsset", searchInScenes);
        var sceneAssets = new List<SceneAsset>();

        foreach (var sceneGuid in sceneGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<SceneAsset>(assetPath));
        }
        return sceneAssets;

    }

    [MenuItem("Tools/Update Scene Assets")]
    static void UpdateSceneAssets()
    {
        if (!AssetDatabase.IsValidFolder(Path.Combine("Assets", "ScriptableObjects")))
        {
            AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
        }

        foreach (var sceneAsset in GetSceneAssets())
        {
            var sceneData = ScriptableObject.CreateInstance<LevelData>();
            sceneData.SceneAsset = sceneAsset;
            string assetPath = Path.Combine("Assets", "ScriptableObjects", sceneData.sceneName + ".asset");
            AssetDatabase.CreateAsset(sceneData, assetPath);
        }

    }

    static string[] searchInSOFolder = new[] { "Assets/ScriptableObjects/" };
    static List<LevelData> GetSOSceneAssets()
    {

        string[] levelDataGuids = AssetDatabase.FindAssets("t:LevelData", searchInSOFolder);
        var sceneAssets = new List<LevelData>();

        foreach (var levelDataGuid in levelDataGuids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(levelDataGuid);
            sceneAssets.Add(AssetDatabase.LoadAssetAtPath<LevelData>(assetPath));
        }
        return sceneAssets;

    }

    [MenuItem("Tools/Refresh Scene Assets")]
    static void RefreshSceneAssets()
    {
        foreach (var levelAsset in GetSOSceneAssets())
        {
            if (levelAsset.sceneAsset.name != levelAsset.sceneName)
            {
                levelAsset.sceneName = levelAsset.sceneAsset.name;

                //Aide pour changer le nom du SO (faire nouveau SO completement?)
                levelAsset.name.Replace(levelAsset.name, levelAsset.sceneName); 
                
            }
        }
    }


}
#endif