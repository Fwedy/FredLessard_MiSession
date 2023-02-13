using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp_UIManager : MonoBehaviour
{
    public GameObject instaKill;
    public GameObject doublePoints;

    private List<GameObject> images = new List<GameObject>();

    public bool flashInstaKill = false;

    public float flashDuration = 5.0f; // duration of the flash in seconds
    public float flashInterval = 0.05f; // interval between changes in alpha value
    private Image image;
    private Color originalColor;

    private void Start()
    {
        images.Add(instaKill);
        images.Add(doublePoints);

        foreach (GameObject i in images)
        {
            i.SetActive(false);
            
        }
    }

    public void ImageSwitch(GameObject i)
    {
        if (i.activeInHierarchy)
            i.SetActive(false);
        else
            i.SetActive(true);

        i.GetComponent<Animator>().SetBool("Flash", false);
    }

    public void StartFlashing(GameObject o)
    {
        o.GetComponent<Animator>().SetBool("Flash", true);
    }

   




}
