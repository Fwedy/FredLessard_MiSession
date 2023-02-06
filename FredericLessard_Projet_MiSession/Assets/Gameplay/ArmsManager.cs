using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsManager : MonoBehaviour
{
    public GameObject backHand;
    public GameObject currentGun;



    private void Start()
    {
        Resources.Load<Sprite>("2");
        currentGun = backHand.transform.GetChild(0).gameObject;
        
    }
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - backHand.transform.position.x,
            mousePosition.y - backHand.transform.position.y
        );

        float angle = Vector2.SignedAngle(Vector2.right, direction);
        backHand.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

         
    }

    private void ChangeGun(GameObject newGun)
    {
        currentGun = newGun;
    }


}
