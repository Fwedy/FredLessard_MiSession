using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

    

public class GunBase : MonoBehaviour
{
    private GameManager gameManager;

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
    
    public float fireSpeed;
    private bool automatic;
    private bool autoShooting = false;
    private bool currentlyShooting = false;
    public string gunName;

    [SerializeField] TextMeshProUGUI ammoTXT;
    public float ammoInMag;
    private float ammoInStash;
    private bool reloading = false;

    [SerializeField] public AudioClip shootSFX;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        muzzle = transform.GetChild(0).gameObject;
        bulletPool = GameObject.FindGameObjectWithTag("BulletPool").GetComponent<ObjectPool_Bullets>();
        bulletType = bulletType.GetComponent<BulletBase>();
        armsManager = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).GetComponent<ArmsManager>();
        ammoTXT = GameObject.FindGameObjectWithTag("AmmoTXT").GetComponent<TextMeshProUGUI>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
       // shootSFX = GetComponent<AudioSource>();

        this.fireSpeed = GunSO.fireFreq;
        this.gunName = GunSO.gunName;
        this.ammoInMag = GunSO.magSize;
        this.ammoInStash = GunSO.storedAmmo;
        this.automatic = GunSO.automatic;

        ammoTXT.text = ammoInMag + "/" + ammoInStash;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && activeGun && canShoot && !armsManager.paused && !automatic)
        {
            canShoot = false;
            Shoot();
        }

        if (Input.GetMouseButtonDown(0) && activeGun && canShoot && !armsManager.paused && automatic)
        {
            autoShooting = true;
            canShoot = false;
            StartCoroutine(AutoShoot());

        }

        if (Input.GetMouseButtonUp(0) && activeGun  && !armsManager.paused && automatic)
        {
            autoShooting = false;
            
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

    IEnumerator AutoShoot()
    {
        if (!currentlyShooting)
        {
            currentlyShooting = true;
            while (autoShooting)
            {
                Shoot();
                yield return new WaitForSeconds(fireSpeed);
            }
            yield return new WaitForSeconds(0.1f);
            canShoot = true;
            currentlyShooting = false;
        }
    }

    public void Shoot()
    {

        StartCoroutine(ShootDelay());
        var newBullet = bulletPool.FindAvailableBullet();
            
            newBullet.transform.position = muzzle.transform.position;
            newBullet.transform.rotation = muzzle.transform.rotation;


            Rigidbody2D rigidBody = newBullet.GetComponent<Rigidbody2D>();
            rigidBody.AddForce(gameObject.transform.right * bulletType.bulletSO.speed);

         ammoInMag -= 1;
        ammoTXT.text = ammoInMag + "/" + ammoInStash;

        if (shootSFX != null)
            AudioSource.PlayClipAtPoint(shootSFX, this.transform.position);

        if (ammoInMag <= 0)
        {
            reloading = true;
            canShoot = false;
            autoShooting = false;
            if (ammoInStash > 0)
                StartCoroutine(Reload());
            
        }       
        
    }

    IEnumerator ShootDelay() {
        

        yield return new WaitForSeconds(fireSpeed);

        if (!reloading)
            canShoot = true;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(gameManager.reloadSpeed);
        if (ammoInStash < ammoInMag)
        {
            ammoInMag = ammoInStash;
            ammoInStash = 0;
        }
        else {
            ammoInMag = GunSO.magSize;
            ammoInStash -= ammoInMag;
        }
        ammoTXT.text = ammoInMag + "/" + ammoInStash;
        canShoot = true;
        reloading = false;
    }

    public void MaxAmmo()
    {
        ammoInMag = GunSO.magSize;
        ammoInStash = GunSO.storedAmmo;
        ammoTXT.text = ammoInMag + "/" + ammoInStash;
        canShoot = true;
        reloading = false;
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
