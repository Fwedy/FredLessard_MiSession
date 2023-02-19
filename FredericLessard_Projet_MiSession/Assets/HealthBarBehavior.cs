using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{
  [SerializeField]  private Image fillImage;


    private void Start()
    {
       // fillImage = gameObject.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
    }
    public void HealthChange(float newHealth) 
    {
        //float x = ((1 / 4) * newHealth);
        fillImage.fillAmount = ((1f / 4f) * newHealth);
        
        
    }

}
