using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
     public float speed;
    [SerializeField] float damage;

    private ObjectPool_Bullets bulletPool;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool_Bullets>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        StartCoroutine(VisibilityDisable());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool x = true; 
        if (collision.tag.Contains("Zombie") && x)
        {
            collision.gameObject.GetComponent<ZombieManager>().ZombTakeDamage(10);
            x = false;
            gameObject.SetActive(false);
        }
        
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
