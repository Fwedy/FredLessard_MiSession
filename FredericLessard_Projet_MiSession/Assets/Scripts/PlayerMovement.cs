using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float currentSpeed = 0f;
    public float walkSpeed = 3f;
    
    
    private SpriteRenderer spriteRenderer;
    public Animator animator;
    private Rigidbody2D rb;

    [SerializeField] private ArmsManager armsManager;
    private GameObject backHand;

    private bool dead = false;
    private bool paused = false;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        backHand = armsManager.backHand;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (!dead && !paused)
        {

            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal != 0 || vertical != 0)
            {
                currentSpeed = walkSpeed;
            }
            else
            {
                currentSpeed = 0.0f;
            }


            Vector3 movement = (transform.right * horizontal + transform.up * vertical) * currentSpeed;
            // transform.position += movement;
            rb.velocity = new Vector2(movement.x, movement.y);


            Vector3 mousePosition = Input.mousePosition;
            Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));


            if (screenToWorld.x < transform.position.x)
            {

                spriteRenderer.flipX = true;
                backHand.GetComponent<SpriteRenderer>().flipX = true;
                backHand.transform.localPosition = new Vector3(-0.121f, backHand.transform.localPosition.y, backHand.transform.localPosition.z);
                armsManager.currentGun.GetComponent<GunBase>().FlipLeft();
            }
            else
            {

                spriteRenderer.flipX = false;
                armsManager.backHand.GetComponent<SpriteRenderer>().flipX = false;
                backHand.transform.localPosition = new Vector3(0.121f, backHand.transform.localPosition.y, backHand.transform.localPosition.z);
                armsManager.currentGun.GetComponent<GunBase>().FlipRight();
            }






            animator.SetFloat("Speed", currentSpeed);
        }
    }

    public void PlayerDead()
    {
        dead = true;
        animator.SetTrigger("Death");
        backHand.SetActive(false);
       
    }

    public void PauseGame()
    {
        paused = !paused;
        armsManager.paused = paused;
    }

    public void ActivateSlowDown()
    {
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        float x = walkSpeed;

        walkSpeed = 2f;

        yield return new WaitForSeconds(10);

        walkSpeed = x;
    }

   
}
