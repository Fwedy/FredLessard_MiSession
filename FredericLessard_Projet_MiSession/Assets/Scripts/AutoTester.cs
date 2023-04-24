using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTester : MonoBehaviour
{
    [SerializeField] private ArmsManager armsManager;
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private BoxCollider2D mapBounds;
    private GameManager gameManager;
    public float speed = 3f;

    public Vector2 targetPosition;
    private bool autoTesting = false;
    private Vector2 direction;

    private float pause = 0f;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F4))
        {
            
            StartCoroutine(StartTester());
        }

        if (pause > 0)
        {
            pause -= Time.deltaTime;
            return;
        }
        if (autoTesting)
        {
            if (Vector2.Distance((Vector2)player.transform.position, targetPosition) <= 0.5f)
            {
                pause = 3f;
                targetPosition = RandomPointInBounds(mapBounds.bounds);
                direction = targetPosition - (Vector2)transform.position;
                direction.Normalize();
            }
            
            Vector2 velocity = direction * speed;
            playerRB.velocity = velocity;
        }

    }


    private IEnumerator StartTester()
    {
        if (!autoTesting)
        {
            armsManager.testMode = true;
            Time.timeScale = 2;
            autoTesting = true;
            StartCoroutine(AutoShoot());
            StartCoroutine(AutoWalk());
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1);
        }
        else
        {
            armsManager.testMode = false;
            Time.timeScale = 1;
            autoTesting = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    private IEnumerator AutoShoot()
    {
        armsManager.currentGun.GetComponent<GunBase>().shootSFX = null;
        for (int i = 0; i > -1; i++) {
            if (gameManager.enemies.Count > 0)
            {
                armsManager.currentGun.GetComponent<GunBase>().Shoot();
                armsManager.currentGun.GetComponent<GunBase>().ammoInMag += 1;

            }
            yield return new WaitForSeconds(armsManager.currentGun.GetComponent<GunBase>().fireSpeed * 2);
            if (!autoTesting)
            {
                yield break;
            }
        }
       

    }

    private IEnumerator AutoWalk()
    {
        
        targetPosition = RandomPointInBounds(mapBounds.bounds);
        direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();
        yield break;
        

     }

        public static Vector2 RandomPointInBounds(Bounds bounds)
    {
        return new Vector2(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y)
        );
    }
}
