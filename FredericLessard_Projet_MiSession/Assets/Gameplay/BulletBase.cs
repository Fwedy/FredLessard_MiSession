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
    IEnumerator VisibilityDisable()
    {
        yield return new WaitForSeconds(1);
        if (!spriteRenderer.isVisible)
        {
            gameObject.SetActive(false);
        }
        
    }
}
