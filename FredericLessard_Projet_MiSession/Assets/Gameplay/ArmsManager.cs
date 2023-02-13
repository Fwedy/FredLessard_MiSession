using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsManager : MonoBehaviour
{
    public GameObject backHand;
    public GameObject currentGun;
    public GameObject starterGun;

    private ObjectPool_Bullets bulletPool;

    private void Start()
    {
        Resources.Load<Sprite>("2");
        var gunInstance = Instantiate(starterGun);
        currentGun = gunInstance;
        gunInstance.transform.SetParent(backHand.transform);
        currentGun.transform.localRotation = Quaternion.Euler(0, 0, -90);
        currentGun.transform.localPosition = new Vector3(transform.position.x, transform.position.y, -0.03f);
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool_Bullets>();
        currentGun.GetComponent<GunBase>().activeGun = true;
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - backHand.transform.position.x,
            mousePosition.y - backHand.transform.position.y
        );

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        backHand.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

         
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
