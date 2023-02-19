using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoxScript : MonoBehaviour
{
   private GameManager gameManager;

    private GameObject gunRoller;
    [SerializeField] private int rollLenght;

    public List<GameObject> guns = new List<GameObject>();
    public List<Sprite> gunS = new List<Sprite>();

    private bool playerInRange = false;
    private bool active = true;

    [SerializeField] private int boxCost;

    private TextMeshProUGUI infoTXT;
        // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gunRoller = gameObject.transform.GetChild(0).gameObject;
        gunRoller.SetActive(false);
        
        infoTXT = GameObject.FindGameObjectWithTag("InfoTXT").GetComponent<TextMeshProUGUI>();

        foreach (GameObject gun in guns)
        {
            gunS.Add(gun.GetComponent<SpriteRenderer>().sprite);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && active)
        {
            Debug.Log("Testttt");
            playerInRange = true;
            infoTXT.gameObject.SetActive(true);
            infoTXT.text = "Buy gun box for " + boxCost + " points.";
        }else if (collision.gameObject.tag == "Player" && !active)
        {
            infoTXT.gameObject.SetActive(true);
            infoTXT.text = "Gun box is unavailable, come back later.";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            infoTXT.gameObject.SetActive(false);
        }
    }

    IEnumerator rollSprites()
    {
        int x = 0;
        gunRoller.SetActive(true);

        infoTXT.gameObject.SetActive(true);
        infoTXT.text = "Are you feeling lucky today?";

        for (int i = 0; i < rollLenght; i++)
        {
            var r = Random.Range(0, guns.Count);
            gunRoller.GetComponent<SpriteRenderer>().sprite = guns[r].GetComponent<SpriteRenderer>().sprite;
            yield return new WaitForSeconds(0.3f);
            if (i >= rollLenght)
                x = i;
        }

        GenerateLastGun(x);
        yield return new WaitForSeconds(1.5f);
        active = true;
    }

    private void GenerateLastGun(int gunPos)
    {
        var gun = Instantiate(guns[gunPos], new Vector3(gunRoller.transform.position.x,  gunRoller.transform.position.y, -0.9f ), Quaternion.identity);
        gunRoller.SetActive(false);
        infoTXT.gameObject.SetActive(true);
        infoTXT.text = "Pick up your new " + gun.GetComponent<GunBase>().gunName;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && playerInRange && active)
        {
            if (gameManager.playerPoints >= 950)
            {
                gameManager.ModifyPoints(-boxCost);
                StartCoroutine(rollSprites());
                active = false;
                infoTXT.gameObject.SetActive(true);
                infoTXT.text = "Thank you for your purchase!";
            }
            else
            {
                infoTXT.gameObject.SetActive(true);
                infoTXT.text = "Too broke, sorry!";
            }
        }
    }
}
