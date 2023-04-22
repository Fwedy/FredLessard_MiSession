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
    public string EN;
    public string FR;
    public string ES;
    public string IT;
    public string DE;
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

    private Dictionary<string, int> LanguageColumn = new Dictionary<string, int>()
    {
        { "en", 1 },
        { "fr", 2 },
        { "es", 3 },
        { "it", 4 },
        { "de", 5 },
    };

    private void Start()
    {
        SetLanguage("en");
    }

    public void LoadStrings()
    {
        string fileContents = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "a.tsv"));
        string[] lines = fileContents.Split('\n');

        localizedStrings = new Dictionary<string, string>();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] fields = lines[i].Split('\t');
            string key = fields[0];
            string localizedString = fields[(int)LanguageColumn[currentLanguage]].Trim();
            localizedStrings[key] = localizedString;
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
