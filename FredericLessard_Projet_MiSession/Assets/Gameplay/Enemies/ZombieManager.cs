using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie_Base : MonoBehaviour
{
    protected float HP;
    protected float maxHP;
    protected float speed;
    protected Rigidbody2D rb;
    protected GameManager gameManager;

    public float GetSpeed()
    {
        return speed;
    }

    public virtual void TakeDamage(int damage) { }

    public virtual void Death() { }

    public virtual void Attack() { }

    public virtual void HPMultiplier(float multiplier) { }

    public virtual void SpawnSpeed() { }

    public virtual void SpeedMultiplier(float multiplier) { }


    //public abstract ZombieEnemy_Base ZombieFactory();


}
public class ZombieCrawler : Zombie_Base
{
    ZombieMovement zombMovement = new ZombieMovement();
    public ZombieCrawler()
    {
        HP = 30;
        maxHP = 30;
        speed = 1f;
        
    }

    private void Awake()
    {


    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        zombMovement = gameObject.GetComponent<ZombieMovement>();
        SpawnSpeed();
        zombMovement.SetSpeed(this.speed);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public override void SpawnSpeed()
    {
        speed = Random.Range(0.2f, 1.3f);
    }

    public override void TakeDamage(int damage)
    {
        Debug.Log("sd");
        HP -= damage;
        Debug.Log(HP);
        if (HP <= 0)
        {
            Death();
        }
    }

    public override void Death()
    {
        StartCoroutine(DeathCoroutine());
        
    }
    IEnumerator DeathCoroutine()
    {
        gameManager.EnemyDied();
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
    }

    /*public override ZombieBase ZombieFactory()
    {
        return new Zombie_Crawler();
    } */
}

public class RunnerCreature : Zombie_Base
{
    ZombieMovement zombMovement = new ZombieMovement();
    public RunnerCreature()
    {
        HP = 50;
        maxHP = 50;
        speed = 2f;

    }

    private void Awake()
    {


    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        zombMovement = gameObject.GetComponent<ZombieMovement>();
        SpawnSpeed();
        zombMovement.SetSpeed(this.speed);
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }

    public override void SpawnSpeed()
    {
        speed = Random.Range(0.6f, 2.5f);
    }

    public override void TakeDamage(int damage)
    {
        bool x = true;
        HP -= damage;
        Debug.Log(HP);
        if (HP <= 0 && x)
        {
            Death();
            x = false;
        }
    }

    public override void Death()
    {
        StartCoroutine(DeathCoroutine());

    }
    IEnumerator DeathCoroutine()
    {
        gameManager.EnemyDied();
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
    }

    /*public override ZombieBase ZombieFactory()
    {
        return new Zombie_Crawler();
    } */
}

public class ZombieManager : MonoBehaviour
{
    
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
    }

    public void ZombTakeDamage (int dmg)
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
}
