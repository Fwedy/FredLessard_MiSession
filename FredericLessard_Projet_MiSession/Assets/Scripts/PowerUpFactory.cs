using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUps = new List<GameObject>();
    public GameObject maxAmmo;
    public GameObject CreatePowerUp()
    {

        int x = Random.Range(0, powerUps.Count);
        return powerUps[x];
    }

    
}
