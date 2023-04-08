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

public class PromoCodeManager : MonoBehaviour
{

    [SerializeField] private GameObject codePanel;
    [SerializeField] private TextMeshProUGUI codeInputField;
    [SerializeField] private TextMeshProUGUI promoCodeInfoText;

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
            StartCoroutine(GetCode(code));
        }
        else
        {
            promoCodeInfoText.text = "Code format is invalid.";
            promoCodeInfoText.color = Color.red;
        }
    }

    public IEnumerator GetCode(string code)
    {

        /*string json = JsonConvert.SerializeObject(new { Code = codeInputField.text.ToString() });
     
        string uri = "https://parseapi.back4app.com/classes/promocode/?where="+ json;
        */                                                                             //add variable

        string jsonBody = "?where={\"Code\":\"" + code + "\"}";

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        string uri = "https://parseapi.back4app.com/classes/promocode/" ;

            Debug.Log(uri);
        using (var request = UnityWebRequest.Get(uri))
        {
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            
          
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
                //string _code;
                //var match = JsonConvert.SerializeObject(request.downloadHandler.text);


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
                    var oID_matches = Regex.Matches(request.downloadHandler.text, "\"objectId\":(\\w+)", RegexOptions.Multiline);
                    StartCoroutine(ReturnUsedCode("aaa"/*oID_matches.Last().ToString()*/));
                
            }
        }
    }

    public IEnumerator ReturnUsedCode(string objectID)
    {                                                                                              //add variable, not hard coded
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/classes/promocode/ApFz6MSyd5", "PUT"))
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
        }
    }
}
