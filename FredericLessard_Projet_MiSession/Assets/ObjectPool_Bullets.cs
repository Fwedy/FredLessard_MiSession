using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Bullets : MonoBehaviour
{
    [SerializeField] private int maxBullets = 50;

    public List<BulletBase> bulletList = new List<BulletBase>();
    
    [SerializeField] BulletBase bulletBase;

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

    public BulletBase FindAvailableBullet()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            if (!bulletList[i].gameObject.activeInHierarchy)
            {
                return bulletList[i];
                
            }
            
                
        }
        return bulletList[0];
    }

   }
