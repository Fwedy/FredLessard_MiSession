using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI languageSwitcher;
    [SerializeField] private TMP_Dropdown languageDropdown;

    private LanguageLocalization localizer;

    private  Dictionary<string, int> LanguageColumn = new Dictionary<string, int>()
    {
        { "en", 0 },
        { "fr", 1 },
        { "es", 2 },
        { "it", 3 },
        { "de", 4 },
    };
    private void Awake()
    {
        localizer = GameObject.FindGameObjectWithTag("Localizer").GetComponent<LanguageLocalization>();
        languageDropdown.value = LanguageColumn[PersistentData.Deserialize().language];
    }

    public void OnLanguageSwitch()
    {
        localizer.SetLanguage(languageSwitcher.text.ToString().ToLower());
        PersistentData.Serialize(0, "", languageSwitcher.text.ToString().ToLower());
    }
}
