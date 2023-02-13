using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp_Proxy : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;
    private List<GameObject> spawners = new List<GameObject>();
    [SerializeField] private PowerUp_UIManager powerUpPanel;
   [SerializeField] private float instaKillDuration;



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
            spawner.GetComponent<ZombieSpawner>().instaKillActive = true;

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
            spawner.GetComponent<ZombieSpawner>().instaKillActive = false;

        foreach (GameObject enemy in gameManager.enemies)
        {
            enemy.GetComponent<ZombieManager>().NormalHealth();
        }
    }

}
