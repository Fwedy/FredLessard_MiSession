using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBase : MonoBehaviour
{
    protected PowerUp_Proxy proxy;
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
            OnPowerActivate();
            Destroy(gameObject);

        }
    }

    protected virtual void OnPowerActivate() { }

}
