using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerPowerUp : PowerUpBase
{
    protected override void OnPowerActivate()
    {
        if(Random.value < 0.5f)
        {
            proxy.MaxAmmoActivate();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().ActivateSlowDown();
        }
    }
}
