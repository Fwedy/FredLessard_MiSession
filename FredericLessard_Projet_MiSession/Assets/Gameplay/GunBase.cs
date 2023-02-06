using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField] private BulletBase bulletType;
    private ObjectPool_Bullets bulletPool;

    [SerializeField] private float gunLeftPosX;
    [SerializeField] private float gunRightPosX;

    private SpriteRenderer spriteRenderer;
    
    private GameObject muzzle;
    [SerializeField] private Vector3 muzzLeftPos;
    [SerializeField] private Vector3 muzzRightPos;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        muzzle = transform.GetChild(0).gameObject;
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool_Bullets>();
        bulletType = bulletType.GetComponent<BulletBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public void FlipLeft()
    {
        
            transform.localPosition = new Vector3(gunLeftPosX, transform.localPosition.y, transform.localPosition.z);
            spriteRenderer.flipY = true;
        muzzle.transform.localPosition = muzzLeftPos;

    }

    public void FlipRight()
    {
        transform.localPosition = new Vector3(gunRightPosX, transform.localPosition.y, transform.localPosition.z);
        spriteRenderer.flipY = false;
        muzzle.transform.localPosition = muzzRightPos;
    }

    private void Shoot()
    {
        var newBullet = bulletPool.FindAvailableBullet();
        newBullet.gameObject.SetActive(true);
        newBullet.transform.position = muzzle.transform.position;
        newBullet.transform.rotation = muzzle.transform.rotation;


        Rigidbody2D rigidBody = newBullet.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(gameObject.transform.right * bulletType.speed);

        
    }
}
