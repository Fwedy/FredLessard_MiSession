using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private LevelData levelData;

    private void Start()
    {
        List<string> keys = new List<string>() { "Textures" };
        Addressables.LoadAssetsAsync<UnityEngine.Object>(keys, x => { }, Addressables.MergeMode.Union);
    }

    public void GameplayScene() {

        //SceneManager.LoadScene(levelData.sceneName);
        //Addressables.LoadAssetAsync<UnityEngine.Object>(levelData.sceneName).Completed += SceneLoader_Completed;
        Addressables.LoadSceneAsync(levelData.sceneName, LoadSceneMode.Single);
        Addressables.LoadAssetAsync<AudioClip>("Music_ChipMode").Completed += MusicPlayer_Completed;
    }

    private void MusicPlayer_Completed(AsyncOperationHandle<AudioClip> audioClip)
    {
        var audioSource = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioSource>();
        audioSource.clip = audioClip.Result;
        audioSource.Play();
    }

    private void SceneLoader_Completed(AsyncOperationHandle<UnityEngine.Object> obj)
    {
        Addressables.LoadSceneAsync(levelData.sceneName, LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
