using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private TextMeshProUGUI roundTXT;

    private static GameManager instance;

    public int enemiesLeftToSpawn = 10;
    private int enemiesAlive = 10;
    public int roundNumber =  1;
   
    private List<GameObject> spawners = new List<GameObject>();
    public List<GameObject> enemies = new List<GameObject>();

    public int playerPoints = 0;
    public bool doublePoits = false;

    public float reloadSpeed = 3f;

    private GameObject player;
    public float playerMaxHP = 4f;
    [SerializeField]public float playerHP = 4f;
    [SerializeField]private float regenerationRate;
    private float lastDamageTime;
    private float currentRegenerationAmount;
    private bool takingDamage = false;
    [SerializeField] private Image healthBar;
    private bool revivePerk = true;

    [SerializeField] private PowerUp_Proxy powerUpProxy;

    [SerializeField] private GameObject newPointsTXT;
    [SerializeField] private GameObject UIPointsContainer;
    [SerializeField] private TextMeshProUGUI pointsTXT;
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
        player = GameObject.FindGameObjectWithTag("Player");
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

        

        if (Time.time > lastDamageTime + 5f)
        {
            takingDamage = false;
        }

        if (!takingDamage && playerHP < playerMaxHP && playerHP > 0)
        {
            currentRegenerationAmount += regenerationRate * Time.deltaTime;

            int regeneratedHealth = Mathf.RoundToInt(currentRegenerationAmount);

            if (regeneratedHealth > 0)
            {
                playerHP += regeneratedHealth;
                currentRegenerationAmount -= regeneratedHealth;
                healthBar.GetComponent<HealthBarBehavior>().HealthChange(playerHP, playerMaxHP);
            }
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

        ModifyPoints(60);

        int x = Random.Range(0, 30);
        if(x < 1)
        {
            powerUpProxy.SpawnPowerUp(enemy);
        }

        if(enemies.Contains(enemy))
            enemies.Remove(enemy);

        if (enemiesAlive <= 0)
        {
            StartCoroutine(RoundChange());
            
            int a = Random.Range(0, Mathf.Clamp(100-roundNumber, 0, 100));
            if (a < 1)
            {
                powerUpProxy.SpawnMaxAmmo(enemy);
            }
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

    public void ModifyPoints(int p)
    {
        if (doublePoits)
            p *= 2;

        playerPoints += p;
        var pts = Instantiate(newPointsTXT, UIPointsContainer.transform.GetChild(0).GetComponent<RectTransform>().position, Quaternion.identity);
        pts.GetComponent<TextMeshProUGUI>().text = "+" + p;
        pts.transform.SetParent(UIPointsContainer.transform);
        pointsTXT.text = playerPoints.ToString();
    }

    

    public void TakeDamage()
    {
        if (playerHP > 0)
        {
            playerHP -= 0.5f;
            takingDamage = true;
            lastDamageTime = Time.time;
            healthBar.GetComponent<HealthBarBehavior>().HealthChange(playerHP, playerMaxHP);

            StartCoroutine(HurtAnim());

            if (playerHP <= 0)
            {
                player.GetComponent<PlayerMovement>().PlayerDead();
            }
        }
    }

    IEnumerator HurtAnim()
    {
        player.GetComponent<Animator>().SetBool("Hurt", true);
        yield return new WaitForSeconds(0.25f);
        player.GetComponent<Animator>().SetBool("Hurt", false);
    }

}


