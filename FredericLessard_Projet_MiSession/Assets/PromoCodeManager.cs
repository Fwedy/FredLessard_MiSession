using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PromoCodeManager : MonoBehaviour
{

    [SerializeField] private GameObject codePanel;

    public void OnEnterCodePanelClick()
    {
        codePanel.SetActive(true);
    }


    // Start is called before the first frame update
    void Start()
    {

        File.AppendAllText(Application.streamingAssetsPath + "/Codes.txt", "code,Used\n");
        for (int i = 0; i < 33; i++)
        {
            File.AppendAllText(Application.streamingAssetsPath + "/Codes.txt", "D" + Randomizer() + ",FALSE\n");
        }
        for (int i = 0; i < 33; i++)
        {
            File.AppendAllText(Application.streamingAssetsPath + "/Codes.txt", "X" + Randomizer() + ",FALSE\n");
        }
        for (int i = 0; i < 34; i++)
        {
            File.AppendAllText(Application.streamingAssetsPath + "/Codes.txt", "P" + Randomizer() + ",FALSE\n");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (codePanel.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            codePanel.SetActive(false);
        }
    }

    string Randomizer()
    {
        string x = null;
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        for (int i = 0; i <= 4; i++)
        {
            x += characters[Random.Range(0, characters.Length - 1)];
        }
        return x;
    } 
}
