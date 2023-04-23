using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.Text;
using System.Web;
using System.Net.Http;
using System;
using UnityEngine.UI;

public class PromoCodeManager : MonoBehaviour
{

    [SerializeField] private GameObject codePanel;
    [SerializeField] private TextMeshProUGUI codeInputField;
    [SerializeField] private TextMeshProUGUI promoCodeInfoText;

    //rewards
    [SerializeField] private Image backgroundPanel;

    private string usedCode = null;
    public void OnEnterCodePanelClick()
    {
        codePanel.SetActive(true);
    }


   

    // Update is called once per frame
    void Update()
    {
        if (codePanel.activeInHierarchy == true && Input.GetKeyDown(KeyCode.Escape))
        {
            codePanel.SetActive(false);
        }
    }

    public void OnActivateCodeClick()
    {
        if (codeInputField.text.Length == 7)
        {
            string code = codeInputField.text;
            if (!PersistentData.Deserialize().codeTypes.Contains(code[0]))
                StartCoroutine(GetCode(code));
            else
            {
                promoCodeInfoText.text = "Code type has already been used.";
                promoCodeInfoText.color = Color.red;
            }
        }
        else
        {
            promoCodeInfoText.text = "Code format is invalid.";
            promoCodeInfoText.color = Color.red;
        }
    }

    public IEnumerator GetCode(string code)
    {

        usedCode = code;                                                                        
        code = Uri.EscapeUriString("{\"Code\":\"" + code.Replace("\u200B", "") + "\"}");
    
        using (var request = new WebRequestBuilder()
            .SetUrl("classes/promocode/?where=" + code)
            .SetType("GET")
            .SetDownloadHandler(new DownloadHandlerBuffer())
            .Build()
            ) { 

            yield return request.SendWebRequest();
            
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error:" + request.error);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);

            

            if (request.downloadHandler.text.Length <= 15)
            {
                promoCodeInfoText.text = "This code does not exist";
                promoCodeInfoText.color = Color.red;
                yield break;
               
            }
           
                var used_matches = Regex.Matches(request.downloadHandler.text, "\"Used\":(\\w+)", RegexOptions.Multiline);
                


                bool used = bool.Parse(used_matches.Last().Groups[1].Value);
                if (used)
                {
                    promoCodeInfoText.text = "This code has beed used already.";
                    promoCodeInfoText.color = Color.red;

                }
                else
                {
                    promoCodeInfoText.text = "Code Redeemed!";
                    promoCodeInfoText.color = Color.green;
                    var oID_matches = Regex.Match(request.downloadHandler.text, "\"objectId\":\"(\\w+)\"", RegexOptions.Multiline);
                     Debug.Log(oID_matches.Groups[1].Value);
                    StartCoroutine(ReturnUsedCode(oID_matches.Groups[1].Value));
                
            }
        }
    }

    public IEnumerator ReturnUsedCode(string objectID)
    {                   
       
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/promocode/"+objectID, "PUT"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(new { Used = true });

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Update DIDNT WORK");
                yield break;
            }
            
            Debug.Log(request.downloadHandler.text);
            GiveReward();
        }
    }

    private void GiveReward()
    {
        if (usedCode[0] == 'P')
        {
            PersistentData.Serialize(100, "P", null);

            gameObject.GetComponent<SavedData_MM>().ReloadCoins();
        }else if (usedCode[0] == 'D')
        {
            PersistentData.Serialize(100, "D", null);
            backgroundPanel.color = Color.gray;
        }
        else
        {
            PersistentData.DeleteSaveFile();
            gameObject.GetComponent<SavedData_MM>().ReloadCoins();
        }
        
    }
}
