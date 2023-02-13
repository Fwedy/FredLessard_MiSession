using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillPowerUp : MonoBehaviour
{
    private PowerUp_Proxy proxy;
    private bool x = true;
    void Start()
    {
        proxy = GameObject.FindGameObjectWithTag("PowerUp_Proxy").GetComponent<PowerUp_Proxy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (x)
        {
            x = false;
            proxy.InstaKillActivate();
            Destroy(gameObject);

        }
    }


}
