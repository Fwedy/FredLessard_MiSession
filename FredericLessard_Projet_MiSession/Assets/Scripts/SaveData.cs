using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData
{
    public int coins;
    public string codeTypes;

    public SaveData(int coins, string codeType)
    {
        this.coins = coins;
        this.codeTypes = codeType;
    }
    
}
