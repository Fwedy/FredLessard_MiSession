using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private LevelData levelData;
    public void GameplayScene() {

        SceneManager.LoadScene(levelData.sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
