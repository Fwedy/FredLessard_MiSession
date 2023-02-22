using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
  [SerializeField]  private Image fillImage;
    [SerializeField] private Image container;

    private float x;

    private void Start()
    {
        fillImage.color = new Color32(255, 255, 255, 0);
        container.color = new Color32(255, 255, 255, 0);
    }
    public void HealthChange(float newHealth, float maxHealth) 
    {
         x = ((1f / maxHealth) * newHealth);
        fillImage.fillAmount = x;
        
        if (x >= 0.9f)
        {
            StartCoroutine(HideBarDelay());
        }
        else
        {
            fillImage.color = new Color32(255, 255, 255, 255);
            container.color = new Color32(255, 255, 255, 255);
        }
    }

    IEnumerator HideBarDelay()
    {
        yield return new WaitForSeconds(1.5f);
        if (x >= 0.9f)
        {
            fillImage.color = new Color32(255, 255, 255, 0);
            container.color = new Color32(255, 255, 255, 0);
        }
    }

}
