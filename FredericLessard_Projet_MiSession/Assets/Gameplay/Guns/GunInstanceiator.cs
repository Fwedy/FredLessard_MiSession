using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInstanceiator : MonoBehaviour
{
    public GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
       var inst = Instantiate(myPrefab, transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
