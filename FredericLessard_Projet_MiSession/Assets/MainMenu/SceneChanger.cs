using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private GameObject OptionsMenu;

    public void GameplayScene() {

        SceneManager.LoadScene("Gameplay_Scene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!OptionsMenu.activeInHierarchy)
                OptionsMenu.SetActive(true);
        }
    }
}
