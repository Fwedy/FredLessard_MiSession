using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DoorScript : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private int doorCost;
    [SerializeField] private Sprite openedSprite;
    private SpriteRenderer sr;

    private bool playerInRange = false;
    private bool active = true;
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            playerInRange = false;
    }

    private void Update()
    {
        if (active && playerInRange && Input.GetKey(KeyCode.E))
        {
            if (gameManager.playerPoints >= doorCost)
            {
                active = false;
                gameManager.ModifyPoints(-doorCost);
                gameObject.transform.GetChild(0).GetComponent<Collider2D>().enabled = false;
                AstarPath.active.UpdateGraphs(gameObject.transform.GetChild(0).GetComponent<Collider2D>().bounds);
                sr.sprite = openedSprite;
            }
        }
    }
}
