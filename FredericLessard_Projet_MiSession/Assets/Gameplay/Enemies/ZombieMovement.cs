using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Transform playerTransform;
   [SerializeField] private float speed;
   [SerializeField] private float weight;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ZombieManager zombieScript;
    private Rigidbody2D rb;

    public bool dead = false;
    private void Start()
    {
        //speed = Random.Range(1f, 3.5f);
        zombieScript = gameObject.GetComponent<ZombieManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        rb = GetComponent<Rigidbody2D>();
        rb.mass = weight;

        
    }

    private void Update()
    {
        if (!dead)
        {
           Vector2 direction = playerTransform.position - transform.position;
           // transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);

            if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
        animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("Speed", speed);
    }
}
