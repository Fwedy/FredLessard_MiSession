using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SavedData_MM : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsTXT;
    [SerializeField] private Image backgroundPanel;

    // Start is called before the first frame update
    void Start()
    {
        coinsTXT.text = PersistentData.Deserialize().coins.ToString();
         
        if (PersistentData.Deserialize().codeTypes.Contains('D'))
        {
            backgroundPanel.color = Color.gray;
        }

       GameObject.FindGameObjectWithTag("Localizer").GetComponent<LanguageLocalization>().SetLanguage(PersistentData.Deserialize().language);
    }

    public void ReloadCoins()
    {
        coinsTXT.text = PersistentData.Deserialize().coins.ToString();
    }
}
