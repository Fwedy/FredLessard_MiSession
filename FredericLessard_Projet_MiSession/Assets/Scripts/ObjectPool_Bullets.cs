using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Bullets : MonoBehaviour
{
    [SerializeField] private int maxBullets = 50;

    public List<GameObject> bulletList = new List<GameObject>();
    
    [SerializeField] GameObject bulletBase;

    public bool damageBoostPerk = false;
    

    void Start()
    {

        //Créer les balles au début du jeu
        for (int i = 0; i < maxBullets; i++)
        {
            var newBullet = Instantiate(bulletBase);
            newBullet.gameObject.SetActive(false);
            bulletList.Add(newBullet);

        }
    }

    // Active une nouvelle balle quand le joueur tire
    public GameObject FindAvailableBullet()
    {
        for (int i = 0; i < maxBullets; i++)
        {
            if (!bulletList[i].gameObject.activeInHierarchy)
            {
                bulletList[i].SetActive(true);

                if (damageBoostPerk && bulletList[i].GetComponent<BulletBase>().damage == bulletList[i].GetComponent<BulletBase>().bulletSO.damage)
                {
                    bulletList[i].GetComponent<BulletBase>().damage = bulletList[i].GetComponent<BulletBase>().bulletSO.damage * 2;
                }

                return bulletList[i];
                
            }
            
                
        }
        return bulletList[40];
    }

    //Changer le type de balles quand le joueur change de fusil
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
            
            if (damageBoostPerk)
            {
                temp.GetComponent<BulletBase>().damage *= 2;
            }
        }

        bulletBase = newBullet;
    }

    public void ActivateDamagePerk()
    {
        damageBoostPerk = true;
        foreach (GameObject bullet in bulletList)
        {
            bullet.GetComponent<BulletBase>().damage *= 2;
        }
    } 

   }
