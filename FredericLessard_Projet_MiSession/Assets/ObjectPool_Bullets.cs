using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Bullets : MonoBehaviour
{
    [SerializeField] private int maxBullets = 50;

    public List<GameObject> bulletList = new List<GameObject>();
    
    [SerializeField] GameObject bulletBase;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            var newBullet = Instantiate(bulletBase);
            newBullet.gameObject.SetActive(false);
            bulletList.Add(newBullet);

        }
    }

    public GameObject FindAvailableBullet()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            if (!bulletList[i].gameObject.activeInHierarchy)
            {
                return bulletList[i];
                
            }
            
                
        }
        return bulletList[40];
    }

    public void BulletChange(GameObject newBullet)
    {
        for (int i = 0; i < bulletList.Count; i++)
        {
            Destroy(bulletList[i].gameObject);
            
        }
        bulletList.Clear();
        for (int i = 0; i < maxBullets; i++)
        {
           var temp = Instantiate(newBullet);
            newBullet.gameObject.SetActive(false);
            bulletList.Add(temp);

        }

        bulletBase = newBullet;
    }

 

   }
