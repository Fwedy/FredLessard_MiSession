using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{


    public BulletBase bulletType;
    [SerializeField] GunScriptableObject GunSO;
    private ObjectPool_Bullets bulletPool;
   

    [SerializeField] private Vector2 gunPos;


    private SpriteRenderer spriteRenderer;
    
    private GameObject muzzle;
    [SerializeField] private Vector3 muzzLeftPos;
    [SerializeField] private Vector3 muzzRightPos;

    private bool pickable;
    public ArmsManager armsManager;
    public bool activeGun = false;

    private bool canShoot = true;
    private float fireSpeed; // Fix
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        muzzle = transform.GetChild(0).gameObject;
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool_Bullets>();
        bulletType = bulletType.GetComponent<BulletBase>();
        armsManager = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<ArmsManager>();
        this.fireSpeed = GunSO.fireFreq;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activeGun && canShoot)
        {
            canShoot = false;
            Shoot();
        }

        if (pickable && !activeGun && Input.GetKey(KeyCode.E))
        {
            PickUpGun();
        }
    }
    public void FlipLeft()
    {
        
            transform.localPosition = new Vector3(gunPos.x, gunPos.y, -0.03f);
            spriteRenderer.flipY = true;
        muzzle.transform.localPosition = muzzLeftPos;

    }

    public void FlipRight()
    {
        transform.localPosition = new Vector3(-gunPos.x, gunPos.y, -0.03f);
        spriteRenderer.flipY = false;
        muzzle.transform.localPosition = muzzRightPos;
    }



    private void Shoot()
    {

        StartCoroutine(ShootDelay());
        var newBullet = bulletPool.FindAvailableBullet();
            newBullet.gameObject.SetActive(true);
            newBullet.transform.position = muzzle.transform.position;
            newBullet.transform.rotation = muzzle.transform.rotation;


            Rigidbody2D rigidBody = newBullet.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(gameObject.transform.right * bulletType.bulletSO.speed);

       
        
        
    }

    IEnumerator ShootDelay() {
        

        yield return new WaitForSeconds(fireSpeed);
        canShoot = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
              pickable = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            pickable = false;
    }

    private void PickUpGun()
    {
        armsManager.ChangeGun(gameObject);
        
    }
}
