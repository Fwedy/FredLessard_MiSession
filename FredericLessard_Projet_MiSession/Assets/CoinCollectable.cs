using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameManager.CoinPickup();
        Destroy(gameObject);
    }
}
