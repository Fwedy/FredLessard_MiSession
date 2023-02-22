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

public class EnemyFactory : MonoBehaviour
{
    private AbstractEnemyFactory currrentFactory;

    private CrawlerZombieFactory crawlerFactory;
    private RunnerCreatureFactory runnerFactory;

    [SerializeField] GameObject crawlerPrefab;
    [SerializeField] GameObject runnerPrefab;


  

    // Start is called before the first frame update
    void Start()
    {
        crawlerFactory = gameObject.AddComponent<CrawlerZombieFactory>();
        crawlerFactory.zombiePrefab = crawlerPrefab;
        currrentFactory = crawlerFactory;

        runnerFactory = gameObject.AddComponent<RunnerCreatureFactory>();
        runnerFactory.zombiePrefab = runnerPrefab;
   
    }

    private void RandomizeCreatureSpawns()
    {
        var x = Random.Range(1, 3);
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
        RandomizeCreatureSpawns();
        return currrentFactory.CreateZombie();

    }

    
}

