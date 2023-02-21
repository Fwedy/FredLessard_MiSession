using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp_Proxy : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;
    [SerializeField] private List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private PowerUp_UIManager powerUpPanel;
    [SerializeField] private float instaKillDuration;
    

    
    [SerializeField] private GameObject instaKill;
    [SerializeField] private GameObject maxAmmo;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        spawners.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));

        

    }

    public void InstaKillActivate()
    {
        

        StartCoroutine(InstaKill());
    }

    IEnumerator InstaKill()
    {
        foreach (GameObject spawner in spawners)
            spawner.GetComponent<EnemySpawner_Prefab>().instaKillActive = true;

        foreach (GameObject enemy in gameManager.enemies)
        {
            enemy.GetComponent<ZombieManager>().NewTempHealth(1);
        }

        powerUpPanel.ImageSwitch(powerUpPanel.instaKill);

        yield return new WaitForSeconds(instaKillDuration - 5f);

        powerUpPanel.StartFlashing(powerUpPanel.instaKill);

        yield return new WaitForSeconds(5f);

        powerUpPanel.ImageSwitch(powerUpPanel.instaKill);
        foreach (GameObject spawner in spawners)
            spawner.GetComponent<EnemySpawner_Prefab>().instaKillActive = false;

        foreach (GameObject enemy in gameManager.enemies)
        {
            enemy.GetComponent<ZombieManager>().NormalHealth();
        }
    }

    public void MaxAmmoActivate()
    {
        player.transform.GetChild(0)
        .transform.GetChild(1).transform.GetChild(0)
        .gameObject.GetComponent<GunBase>().MaxAmmo();
    }

}
