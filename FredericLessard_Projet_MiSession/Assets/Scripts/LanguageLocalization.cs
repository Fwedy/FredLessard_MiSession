using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class LanguageData
{
    public string key;
    public string en;
    public string fr;
    public string es;
    public string it;
    public string de;
}

[Serializable]
public class StringsData
{
    public LanguageData[] strings;
}

public class LanguageLocalization : MonoBehaviour
{
    public string currentLanguage = "en";
    private Dictionary<string, string> localizedStrings;

    public List<TextLocalizer> translatedTexts = new List<TextLocalizer>();
    public void LoadStrings()
    {
        string stringFile = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "LanguagesStrings.json"));
        var data = JsonUtility.FromJson<StringsData>(stringFile);

        localizedStrings = new Dictionary<string, string>();
        foreach (var stringData in data.strings)
        {

            switch (currentLanguage)
            {
                case "en":
                    localizedStrings[stringData.key] = stringData.en;
                    break;
                case "fr":
                    localizedStrings[stringData.key] = stringData.fr;
                    break;
                case "es":
                    localizedStrings[stringData.key] = stringData.es;
                    break;
                case "it":
                    localizedStrings[stringData.key] = stringData.it;
                    break;
                case "de":
                    localizedStrings[stringData.key] = stringData.de;
                    break;
            }
        }
    }


    public string GetString(string key)
    {
        if (localizedStrings == null)
        {
            LoadStrings();
        }

        string localizedString;
        if (localizedStrings.TryGetValue(key, out localizedString))
        {
            return localizedString;
        }
        else
        {
            return "";
        }
    }

    public void SetLanguage(string newLanguage)
    {
        if (currentLanguage != newLanguage)
        {
            currentLanguage = newLanguage;
            LoadStrings();

            foreach (TextLocalizer txt in translatedTexts)
            {
                txt.Localize();
            }

        }
    }
}
