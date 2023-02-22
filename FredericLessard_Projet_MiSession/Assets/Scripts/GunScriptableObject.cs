using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "GunSO")]
public class GunScriptableObject : ScriptableObject
{
    public string gunName;
    public float fireFreq;
    public int magSize;
    public int storedAmmo;
    public bool automatic;
    
}
