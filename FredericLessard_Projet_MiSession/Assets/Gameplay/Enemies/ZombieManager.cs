using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public abstract class Zombie_Base : MonoBehaviour
{
    protected float HP;
    protected float maxHP;
    protected float speed;
    protected Rigidbody2D rb;
    protected GameManager gameManager;
    public AIPath path;
    protected ZombieMovement zombMovement = new ZombieMovement();
    protected float tempPrevHealth;
    public float GetSpeed()
    {
        return speed;
    }

    public virtual void Start()
    {

        
        path.maxSpeed = this.GetSpeed();

    }

    public virtual void TakeDamage(float damage) { }

    public virtual void Death() { path.maxSpeed = 0; }

    public virtual void Attack() { }

    public virtual void HPMultiplier(float multiplier) { }

    public virtual void SpawnSpeed() { }

    public virtual void SpeedMultiplier(float multiplier) { }

    public virtual void SetTempHealth(float hp) { }

    public virtual void ReturnNormalHealth() {
        HP = tempPrevHealth;
    }

    //public abstract ZombieEnemy_Base ZombieFactory();


}
public class ZombieCrawler : Zombie_Base
{
    
    public ZombieCrawler()
    {
        HP = 30;
        maxHP = 30;
        speed = 1f;
        tempPrevHealth = 0;
    }

    private void Awake()
    {


    }
    public override void Start()
    {

        rb = gameObject.GetComponent<Rigidbody2D>();
        zombMovement = gameObject.GetComponent<ZombieMovement>();
        SpawnSpeed();
        zombMovement.SetSpeed(this.speed);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        base.Start();
        
    }

    public override void SpawnSpeed()
    {
        speed = Random.Range(0.2f, 1.3f);
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log("sd");
        HP -= damage;
        Debug.Log(HP);
        if (HP <= 0f)
        {
            Death();
        }
    }

    public override void Death()
    {
        base.Death();

        StartCoroutine(DeathCoroutine());
        
    }
    IEnumerator DeathCoroutine()
    {
        gameManager.EnemyDied(gameObject);
        rb.velocity = new Vector2(0, 0);
        var animator = GetComponent<Animator>();
        animator.SetBool("Dead", true);
        zombMovement.dead = true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


    public override void HPMultiplier(float multiplier)
    {
        HP += multiplier;
    }

    public override void SpeedMultiplier(float multiplier)
    {
        speed += multiplier;
        path.maxSpeed = this.speed;
    }


    public override void SetTempHealth(float hp)
    {
        tempPrevHealth = HP;
        HP = hp;
    }


    /*public override ZombieBase ZombieFactory()
    {
        return new Zombie_Crawler();
    } */
}

public class RunnerCreature : Zombie_Base
{
    
    public RunnerCreature()
    {
        HP = 50f;
        maxHP = 50;
        speed = 2f;
        tempPrevHealth = 0;
    }

    private void Awake()
    {


    }
    public override void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        zombMovement = gameObject.GetComponent<ZombieMovement>();
        SpawnSpeed();
        zombMovement.SetSpeed(this.speed);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        base.Start();

    }

    public override void SpawnSpeed()
    {
        speed = Random.Range(0.6f, 2.5f);
    }

    public override void TakeDamage(float damage)
    {
        bool x = true;
        HP -= damage;
        
        if (HP <= 0f && x)
        {
            Death();
            x = false;
        }
    }

    public override void Death()
    {
        base.Death();
        StartCoroutine(DeathCoroutine());

    }
    IEnumerator DeathCoroutine()
    {
        gameManager.EnemyDied(gameObject);
        rb.velocity = new Vector2(0, 0);
        var animator = GetComponent<Animator>();
        animator.SetBool("Dead", true);
        zombMovement.dead = true;
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }


    public override void HPMultiplier(float multiplier)
    {
        HP += multiplier;
    }

    public override void SpeedMultiplier(float multiplier)
    {
        speed += multiplier;
        path.maxSpeed = this.speed;
    }

    public override void SetTempHealth(float hp)
    {
        tempPrevHealth = HP;
        HP = hp;
    }

    /*public override ZombieBase ZombieFactory()
    {
        return new Zombie_Crawler();
    } */
}

public class ZombieManager : MonoBehaviour
{
    private GameManager gameManager;
    private Zombie_Base currentZombie;
    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.tag == "Zombie_Crawler")
        {
            currentZombie = gameObject.AddComponent<ZombieCrawler>();
        }else if(gameObject.tag == "Zombie_Runner")
        {
            currentZombie = gameObject.AddComponent<RunnerCreature>();
        }
        currentZombie.path = gameObject.GetComponent<AIPath>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            
            var d = collision.gameObject.GetComponent<BulletBase>().damage;
            ZombTakeDamage(d);
            collision.gameObject.SetActive(false);
            gameManager.ModifyPoints(10);
        }
    }

    public void ZombTakeDamage (float dmg)
    {
        currentZombie.TakeDamage(dmg);
    }

    public void ZombAddHP(float m)
    {
        currentZombie.HPMultiplier(m);
    }

    public void ZombAddSpeed(float m)
    {
        currentZombie.SpeedMultiplier(m);
        
    }

    public void NewTempHealth(float hp)
    {
        currentZombie.SetTempHealth(hp);
    }

    public void NormalHealth()
    {
        currentZombie.ReturnNormalHealth();
    }
        
}
