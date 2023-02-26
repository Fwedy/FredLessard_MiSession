using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Prefab : MonoBehaviour
{

    private EnemyFactory enemyFactory;
    private GameManager gameManager;

    public bool active;

    public float hpMultiplier;
    public float speedMultiplier;
    public bool instaKillActive = false;

    public float spawnDelay = 4f;
    // Start is called before the first frame update
    void Start()
    {
        enemyFactory = GameObject.FindGameObjectWithTag("EnemyFactory").GetComponent<EnemyFactory>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void TurnOn()
    {
        active = true;
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {

        while (gameManager.enemiesLeftToSpawn > 0 && active)
        {
            
            var newEnemy = enemyFactory.CreateNewZombie();
            newEnemy.transform.position = transform.position;

            newEnemy.GetComponent<ZombieManager>().ZombAddHP(hpMultiplier);
            newEnemy.GetComponent<ZombieManager>().ZombAddSpeed(speedMultiplier);

            gameManager.enemiesLeftToSpawn -= 1;
            gameManager.enemies.Add(newEnemy);

            if (instaKillActive)
            {
                newEnemy.GetComponent<ZombieManager>().NewTempHealth(1);
            }

            yield return new WaitForSeconds(Mathf.Clamp(spawnDelay,0.125f, 4f));

            if (spawnDelay > 0.125f)
                spawnDelay -= 0.125f;
        }

    }
}
