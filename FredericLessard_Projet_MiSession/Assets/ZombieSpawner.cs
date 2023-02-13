using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyFactory : MonoBehaviour
{
    public GameObject zombiePrefab;
    public abstract GameObject CreateZombie();

}

public class CrawlerZombieFactory : AbstractEnemyFactory
{
    
    public override GameObject CreateZombie()
    {
        var zombie = Instantiate(zombiePrefab);
        return zombie;
    }
}

public class RunnerCreatureFactory : AbstractEnemyFactory
{

    public override GameObject CreateZombie()
    {
        var zombie = Instantiate(zombiePrefab);
        return zombie;
    }
}

public class ZombieSpawner : MonoBehaviour
{
    private AbstractEnemyFactory currrentFactory;

    private CrawlerZombieFactory crawlerFactory;
    private RunnerCreatureFactory runnerFactory;

    [SerializeField] GameObject crawlerPrefab;
    [SerializeField] GameObject runnerPrefab;
    private Vector2 spawnerLoc;

    private GameManager gameManager;
    public bool active = false;

    public float hpMultiplier;
    public float speedMultiplier;
    public bool instaKillActive = false;

    // Start is called before the first frame update
    void Start()
    {
        crawlerFactory = gameObject.AddComponent<CrawlerZombieFactory>();
        crawlerFactory.zombiePrefab = crawlerPrefab;
        currrentFactory = crawlerFactory;

        runnerFactory = gameObject.AddComponent<RunnerCreatureFactory>();
        runnerFactory.zombiePrefab = runnerPrefab;
        

        spawnerLoc = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        StartCoroutine(SpawnRoutine());
    }

    private void OnEnable()
    {
        //StartCoroutine(SpawnRoutine());
    }

    private void RandomizeCreatureSpawns()
    {
       var x =  Random.Range(1, 3);
        if (x < 1.5)
        {
            currrentFactory = runnerFactory;
        }
        else
        {
            currrentFactory = crawlerFactory;
        }
    }


    public GameObject CreateNewZombie()
    {

        return currrentFactory.CreateZombie();
        
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
            RandomizeCreatureSpawns();
            var newZomb = CreateNewZombie();
            newZomb.transform.position = spawnerLoc;

            newZomb.GetComponent<ZombieManager>().ZombAddHP(hpMultiplier);
            newZomb.GetComponent<ZombieManager>().ZombAddSpeed(speedMultiplier);

            gameManager.enemiesLeftToSpawn -= 1;
            gameManager.enemies.Add(newZomb);

            if (instaKillActive)
            {
                newZomb.GetComponent<ZombieManager>().NewTempHealth(1);
            }

            yield return new WaitForSeconds(4);


        }
        
    }
}
