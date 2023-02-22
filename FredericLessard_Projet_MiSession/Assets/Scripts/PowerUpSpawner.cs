using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private PowerUpFactory pUFactory;

    private void Start()
    {
        pUFactory = GameObject.FindGameObjectWithTag("PowerUpFactory").GetComponent<PowerUpFactory>();
    }

    public void SpawnPowerUp(GameObject enemy)
    {
        
        Instantiate(pUFactory.CreatePowerUp(), enemy.transform.position, Quaternion.identity);
    }

    public void SpawnMaxAmmo(GameObject enemy)
    {
        Instantiate(pUFactory.maxAmmo, enemy.transform.position, Quaternion.identity);
    }
}
