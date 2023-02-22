using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "BulletSO")]
public class BulletScriptableObject : ScriptableObject
{
    public float speed;
    public float damage;
    public float fireSpeed;
    public int magSize;
    public int storedAmmo;
    public string gunName;

    public Sprite sprite;
  
   
}
