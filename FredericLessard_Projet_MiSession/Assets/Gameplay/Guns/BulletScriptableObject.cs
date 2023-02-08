using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletScriptableObject : ScriptableObject
{
    public float speed;
    public float damage;
    public float fireSpeed;
    public float magSize;
    public float storedAmmo;

    public Sprite sprite;
  
   
}
