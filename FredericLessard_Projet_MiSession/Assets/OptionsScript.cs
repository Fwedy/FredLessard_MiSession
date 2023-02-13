using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] private Slider VolumeSlider;
    
    // Start is called before the first frame update
    void Start()
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameObject.activeInHierarchy)
                CloseOptions();
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
    private void CloseOptions()
    {

        gameObject.SetActive(false);
    }

        
 }
