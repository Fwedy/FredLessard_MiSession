using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTester : MonoBehaviour
{
    [SerializeField] private ArmsManager armsManager;
    private GameManager gameManager;

    public Vector3 targetPosition;
    private bool autoTesting = false;
    private void Update()
    {
        if (Input.GetKey(KeyCode.F4))
        {
            gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            StartCoroutine(StartTester());
        }

    }


    private IEnumerator StartTester()
    {
        if (!autoTesting)
        {
            armsManager.testMode = true;

            autoTesting = true;
            StartCoroutine(AutoShoot());
            yield return new WaitForSeconds(1);
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
}
