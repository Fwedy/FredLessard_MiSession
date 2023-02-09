using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
     private float speed;
     private float damage;

    private ObjectPool_Bullets bulletPool;
    private SpriteRenderer spriteRenderer;

    public BulletScriptableObject bulletSO;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool_Bullets>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bulletSO.sprite;
        this.damage = bulletSO.damage;
        this.speed = bulletSO.speed;
        
    }

    private void OnEnable()
    {
        StartCoroutine(VisibilityDisable());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool x = true; 
        if (collision.gameObject.tag.Contains("Zombie") && x)
        {
            collision.gameObject.GetComponent<ZombieManager>().ZombTakeDamage(10);
            x = false;
            
        }
        gameObject.SetActive(false);
    }


    IEnumerator VisibilityDisable()
    {
        yield return new WaitForSeconds(1);
        if (!spriteRenderer.isVisible)
        {
            gameObject.SetActive(false);
        }
        
    }
}
