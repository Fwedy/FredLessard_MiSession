using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ArmsManager : MonoBehaviour
{
    public GameObject backHand;
    public GameObject currentGun;
    public GameObject starterGun;

    [Inject] private ObjectPool_Bullets bulletPool;

    public bool paused = false;

    private GameManager gameManager;
    public bool testMode = false;
    private void Start()
    {
        Resources.Load<Sprite>("2");
        var gunInstance = Instantiate(starterGun);
        currentGun = gunInstance;
        gunInstance.transform.SetParent(backHand.transform);
        currentGun.transform.localRotation = Quaternion.Euler(0, 0, -90);
        currentGun.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -0.03f);
        //bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool_Bullets>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        currentGun.GetComponent<GunBase>().activeGun = true;
    }
    void Update()
    {
        if (!paused && !testMode)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(
                mousePosition.x - backHand.transform.position.x,
                mousePosition.y - backHand.transform.position.y
            );

            float angle = Vector2.SignedAngle(Vector2.right, direction);
            backHand.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

        }else if(!paused && testMode && gameManager.enemies.Count > 0)
        {
            GameObject nearestEnemy = gameManager.enemies[0];


                foreach (GameObject enemy in gameManager.enemies) {
                        if (Vector2.Distance(enemy.transform.position, gameObject.transform.position) < Vector2.Distance(nearestEnemy.transform.position, gameObject.transform.position))
                         {
                    nearestEnemy = enemy;
                          }

                    }
            Vector2 direction = new Vector2(
                nearestEnemy.transform.position.x - backHand.transform.position.x,
                nearestEnemy.transform.position.y - backHand.transform.position.y
            );

            float angle = Vector2.SignedAngle(Vector2.right, direction);
            backHand.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }

    public void ChangeGun(GameObject newGun)
    {

        backHand.transform.DetachChildren();
        Destroy(currentGun);
        currentGun = newGun;
        currentGun.GetComponent<GunBase>().activeGun = true;
        
        newGun.transform.SetParent(backHand.transform);
        currentGun.transform.localRotation = Quaternion.Euler(0, 0, -90);
        bulletPool.BulletChange(currentGun.GetComponent<GunBase>().bulletType.gameObject);
        
        
    }


}
