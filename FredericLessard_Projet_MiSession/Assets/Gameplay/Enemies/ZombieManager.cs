using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Zombie_Base : MonoBehaviour
{
    protected float HP;
    protected float maxHP;
    protected float speed;

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
        ZombieMovement zombMovement = gameObject.GetComponent<ZombieMovement>();
        SpawnSpeed();
        zombMovement.SetSpeed(this.speed);
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
        Destroy(gameObject);
    }

    public override void HPMultiplier(float multiplier)
    {
        HP *= multiplier;
    }

    public override void SpeedMultiplier(float multiplier)
    {
        speed *= multiplier;
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
    void Start()
    {
        if (gameObject.tag == "Zombie_Crawler")
        {
            currentZombie = gameObject.AddComponent<ZombieCrawler>();
        }
    }

    public void ZombTakeDamage (int dmg)
    {
        currentZombie.TakeDamage(dmg);
    }
}
