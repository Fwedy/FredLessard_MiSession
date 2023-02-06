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

    private void VolumeChanged()
    {
        
        
    }

    private void CloseOptions()
    {
        gameObject.SetActive(false);
    }
        
 }
