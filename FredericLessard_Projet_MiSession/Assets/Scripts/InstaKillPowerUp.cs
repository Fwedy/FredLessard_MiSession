using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstaKillPowerUp : PowerUpBase
{

    protected override void OnPowerActivate()
    {
        proxy.InstaKillActivate();
    }

}
