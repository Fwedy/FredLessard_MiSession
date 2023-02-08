using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Transform playerTransform;
    public float speed;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private ZombieManager zombieScript;
    private void Start()
    {
        //speed = Random.Range(1f, 3.5f);
        zombieScript = gameObject.GetComponent<ZombieManager>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();


        
    }

    private void Update()
    {
        Vector2 direction = playerTransform.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void SetSpeed(float s)
    {
        speed = s;
        animator = gameObject.GetComponent<Animator>();
        animator.SetFloat("Speed", speed);
    }
}