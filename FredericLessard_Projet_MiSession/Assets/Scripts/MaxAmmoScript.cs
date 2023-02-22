using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAmmoScript : MonoBehaviour
{
    private PowerUp_Proxy proxy;
    private bool x = true;
    void Start()
    {
        proxy = GameObject.FindGameObjectWithTag("PowerUp_Proxy").GetComponent<PowerUp_Proxy>();
        Destroy(gameObject, 16);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (x && collision.gameObject.tag == "Player")
        {
            x = false;
            proxy.MaxAmmoActivate();
            Destroy(gameObject);

        }
    }
}
