using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerksProxy : MonoBehaviour
{
   [SerializeField] private GameManager gameManager;
    private GameObject player;
    [SerializeField] private ObjectPool_Bullets bulletPool;

    private List<Image> perks = new List<Image>();

    [SerializeField] private Image healthIcon;
    [SerializeField] private Image speedIcon;
    [SerializeField] private Image damageIcon;
    [SerializeField] private Image quickReloadIcon;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        perks.Add(healthIcon);
        perks.Add(speedIcon);
        perks.Add(damageIcon);
        perks.Add(quickReloadIcon);
    }

    private void Health()
    {
        healthIcon.gameObject.SetActive(true);
        gameManager.playerMaxHP += 2f;
        perks.Remove(healthIcon);
    }

    private void SpeedBoost()
    {
        speedIcon.gameObject.SetActive(true);
        player.GetComponent<PlayerMovement>().walkSpeed += 0.5f;
        perks.Remove(speedIcon);
    }

    private void DamageBoost()
    {
        damageIcon.gameObject.SetActive(true);
        bulletPool.ActivateDamagePerk();
        perks.Remove(damageIcon);
    }

    private void QuickReload()
    {
        quickReloadIcon.gameObject.SetActive(true);
        gameManager.reloadSpeed -= 1.5f;
        perks.Remove(quickReloadIcon);
    }

    public void RollNewPerk()
    {
        if (perks.Count > 0)
            StartCoroutine(PerksRoller());
    }

    IEnumerator PerksRoller()
    {
        yield return new WaitForSeconds(5);

        
        var perk = perks[Random.Range(0, perks.Count)];

        if (perk == healthIcon)
            Health();
        else if (perk == speedIcon)
            SpeedBoost();
        else if (perk == damageIcon)
            DamageBoost();
        else if (perk == quickReloadIcon)
            QuickReload();
                
       }
}
