using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Options_MM : MonoBehaviour
{
    [SerializeField] private Slider VolumeSlider;


    [SerializeField] private GameObject optionsPanel;
    private bool active = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("mainVolume"))
        {
            PlayerPrefs.SetFloat("mainVolume", 1);
            LoadPP();
        }

        else
        {
            LoadPP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active && Input.GetKey(KeyCode.Escape))
        {
            optionsPanel.SetActive(false);
            active = false;
        }
    }
    public void VolumeChanged()
    {
        AudioListener.volume = VolumeSlider.value;
        SavePP();
    }

    private void LoadPP()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("mainVolume");
    }

    private void SavePP()
    {
        PlayerPrefs.SetFloat("mainVolume", VolumeSlider.value);
    }

    public void OnOptionsBTNClick()
    {
        optionsPanel.SetActive(true);
        active = true;
    }
}
