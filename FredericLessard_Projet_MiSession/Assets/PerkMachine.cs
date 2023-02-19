using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerkMachine : MonoBehaviour
{
    private GameManager gameManager;
    private PerksProxy perkProxy;

   [SerializeField] private bool active = true;
   [SerializeField] private bool playerInRange = false;

    public int perkCost = 1500;

    private TextMeshProUGUI infoTXT;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        perkProxy = GameObject.FindGameObjectWithTag("PerkProxy").GetComponent<PerksProxy>();
        infoTXT = GameObject.FindGameObjectWithTag("InfoTXT").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (active && playerInRange && Input.GetKey(KeyCode.E))
        {
            if (gameManager.playerPoints >= perkCost)
            {
                active = false;
                gameManager.ModifyPoints(-perkCost);
                perkProxy.RollNewPerk();
                StartCoroutine(RefreshDelay());
                infoTXT.gameObject.SetActive(true);
                infoTXT.text = "Vending...";
                perkCost += 500;
            }
        }
    }

    IEnumerator RefreshDelay()
    {
        yield return new WaitForSeconds(6);
        infoTXT.gameObject.SetActive(false);
        yield return new WaitForSeconds(54);
        active = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active)
        {
            playerInRange = true;
            infoTXT.gameObject.SetActive(true);
            infoTXT.text = "Purchase perk for " + perkCost + " points.";
        }else if (collision.tag == "Player" && !active)
        {
            infoTXT.gameObject.SetActive(true);
            infoTXT.text = "Perk machine is empty, please come back later...";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInRange = false;
            infoTXT.gameObject.SetActive(false);
        }
    }

    
}
