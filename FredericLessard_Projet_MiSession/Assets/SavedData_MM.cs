using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavedData_MM : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsTXT;

    // Start is called before the first frame update
    void Start()
    {
        coinsTXT.text = PersistentData.Deserialize().coins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
