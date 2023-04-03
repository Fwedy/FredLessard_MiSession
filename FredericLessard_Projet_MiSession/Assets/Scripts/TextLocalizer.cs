using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLocalizer : MonoBehaviour
{
    [SerializeField] string key;

    private LanguageLocalization localizer;
    void Start()
    {
        localizer = GameObject.FindGameObjectWithTag("Localizer").GetComponent<LanguageLocalization>();
        localizer.translatedTexts.Add(this);
        Localize();
    }

    public void Localize()
    {
        GetComponent<TextMeshProUGUI>().text = localizer.GetString(key);
    }
}
