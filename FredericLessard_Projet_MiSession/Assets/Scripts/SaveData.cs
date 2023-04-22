using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int coins;
    public string codeTypes;
    public string language;

    public SaveData(int coins, string codeType, string language)
    {
        this.coins = coins;
        this.codeTypes = codeType;
        this.language = language;
    }
    
}
