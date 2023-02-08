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

public class ZombieSpawner : MonoBehaviour
{
    private AbstractEnemyFactory currrentFactory;

    private CrawlerZombieFactory crawlerFactory;

    [SerializeField] GameObject crawlerPrefab;
    private Vector3 spawnerLoc;
    // Start is called before the first frame update
    void Start()
    {
        crawlerFactory = gameObject.AddComponent<CrawlerZombieFactory>();
        crawlerFactory.zombiePrefab = crawlerPrefab;
        currrentFactory = crawlerFactory;
        spawnerLoc = new Vector3(transform.position.x, transform.position.y, 0);
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject CreateNewZombie()
    {
        return currrentFactory.CreateZombie();
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            
            Instantiate(CreateNewZombie(), spawnerLoc, Quaternion.identity);
            yield return new WaitForSeconds(4);
        }
    }
}
