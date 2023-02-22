using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxAmmoScript : PowerUpBase
{
    protected override void OnPowerActivate()
    {
        proxy.MaxAmmoActivate();
    }
}
