using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LanguageSwitcher : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI languageSwitcher;

    private LanguageLocalization localizer;
    private void Awake()
    {
        localizer = GameObject.FindGameObjectWithTag("Localizer").GetComponent<LanguageLocalization>();
    }

    public void OnLanguageSwitch()
    {
        localizer.SetLanguage(languageSwitcher.text.ToString().ToLower());
    }
}
