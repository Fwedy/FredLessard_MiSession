using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private TextMeshProUGUI roundTXT;

    private static GameManager instance;

    public int enemiesLeftToSpawn = 10;
    private int enemiesAlive = 10;
    private int roundNumber = 0;
   
    private List<GameObject> spawners = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<GameManager>();
            
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);


        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawners.AddRange(GameObject.FindGameObjectsWithTag("Spawner"));
        StartCoroutine(RoundChange());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!OptionsMenu.activeInHierarchy)
                OptionsMenu.SetActive(true);
        }

       
    }

    private void newRound()
    {
        roundNumber += 1;
        enemiesLeftToSpawn = 5 * roundNumber;
        enemiesAlive = enemiesLeftToSpawn;

        roundTXT.text = roundNumber.ToString();
    }

    public void EnemyDied(GameObject enemy)
    {
        enemiesAlive -= 1;

        if(enemies.Contains(enemy))
            enemies.Remove(enemy);

        if (enemiesAlive <= 0)
        {
            StartCoroutine(RoundChange());
        }
    }

    IEnumerator RoundChange()
    {
        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<ZombieSpawner>().active = false;
        }

        yield return new WaitForSeconds(5);
        newRound();

        foreach (GameObject spawner in spawners)
        {
            spawner.GetComponent<ZombieSpawner>().TurnOn();
            spawner.GetComponent<ZombieSpawner>().hpMultiplier = roundNumber * 10;
            spawner.GetComponent<ZombieSpawner>().speedMultiplier += 0.10f;
        }
    }

    
    }
    

