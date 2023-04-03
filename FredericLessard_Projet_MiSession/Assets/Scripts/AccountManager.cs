using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;

public class AccountManager : MonoBehaviour
{
    //Sign Up Fields
    [SerializeField] private GameObject signUpPanel;
    [SerializeField] private TextMeshProUGUI signUpEmailField;
    [SerializeField] private TextMeshProUGUI signUpPswdField;
    [SerializeField] private TextMeshProUGUI SU_InfoTxt;

    //SignIn Fields
    [SerializeField] private GameObject signInPanel;
    [SerializeField] private TextMeshProUGUI signInEmailField;
    [SerializeField] private TextMeshProUGUI signInPswdField;
    [SerializeField] private TextMeshProUGUI SI_InfoTxt;

    private GameObject activePanel;

    private string email = null;
    private string password = null;

    // Start is called before the first frame update
    void Start()
    {
        activePanel = signUpPanel;
    }

    // Update is called once per frame
    void Update()
    {
        if (activePanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape))
        {
            activePanel.SetActive(false);
            SU_InfoTxt.text = "";
            SI_InfoTxt.text = "";

        }

    }

    public void OnSignUpPanelClick()
    {
        signUpPanel.SetActive(true);
        activePanel = signUpPanel;
    }
    public void OnSignInPanelClick()
    {
        signInPanel.SetActive(true);
        activePanel = signInPanel;
    }

    public void OnSignUp()
    {
        password = signUpPswdField.text.ToString();
        email = signUpEmailField.text.ToString();

        if (email != null && password != null)
        {
            StartCoroutine(CreateNewAccount());
        }
        else
        {
            SU_InfoTxt.text = "Some input information is missing.";
            SU_InfoTxt.color = Color.red;
        }
    }

    public void OnSignIn()
    {
        password = signInPswdField.text.ToString();
        email = signInEmailField.text.ToString();
        if (email != null && password != null)
        {
            StartCoroutine(SignIn());
        }
        else
        {
            SI_InfoTxt.text = "Some input information is missing.";
            SI_InfoTxt.color = Color.red;
        }
    }

    public void OnResendEmailVerification()
    {
        email = signUpEmailField.text.ToString();
        if (email != null && password != null)
        {
            StartCoroutine(EmailVerification());
        }
    }


    public IEnumerator CreateNewAccount()
    {
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/users", "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            request.SetRequestHeader("Content-Type", "application/json");

           
            var json = JsonConvert.SerializeObject(new {password = password, email = email, username = email });
            

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                SU_InfoTxt.text = request.error;
                SU_InfoTxt.color = Color.red;
                yield break;
            }
            SU_InfoTxt.text = "SignUp Successful! Please Sign-In";
            SU_InfoTxt.color = Color.green;
            Debug.Log(request.downloadHandler.text);
            
        }
    }

    public IEnumerator EmailVerification()
    {
        
        using (var request = new UnityWebRequest("https://parseapi.back4app.com/verificationEmailRequest", "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("Content-Type", "application/json");
            var json = JsonConvert.SerializeObject(new {email = email});

            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                SU_InfoTxt.text = request.error;
                SU_InfoTxt.color = Color.red;
                yield break;
            }
            SU_InfoTxt.text = "Email verification link sent!";
            SU_InfoTxt.color = Color.green;
            Debug.Log(request.downloadHandler.text);
        }
    }

    public IEnumerator SignIn()
    {

        using (var request = new UnityWebRequest("https://parseapi.back4app.com/login", "POST"))
        {
            request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
            request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
            request.SetRequestHeader("X-Parse-Revocable-Session", "1");
            var json = JsonConvert.SerializeObject(new { username = email, password = password });
            
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                SI_InfoTxt.text = request.error;
                SI_InfoTxt.color = Color.red;
                yield break;
            }
            SI_InfoTxt.text = "Signed In Successfully";
            SI_InfoTxt.color = Color.green;
            Debug.Log(request.downloadHandler.text);
        }
    }

    /* public IEnumerator GetDeathTracker()
     {
         string uri = "https://parseapi.back4app.com/classes/DeathTracker/?where={\"Name\":\"Enemy\"}";
         using (var request = UnityWebRequest.Get(uri))
         {
             request.SetRequestHeader("X-Parse-Application-Id", Secrets.ApplicationId);
             request.SetRequestHeader("X-Parse-REST-API-Key", Secrets.RestApiKey);
             yield return request.SendWebRequest();
             if (request.result != UnityWebRequest.Result.Success)
             {
                 Debug.LogError(request.error);
                 yield break;
             }
             Debug.Log(request.downloadHandler.text);
             var matches = Regex.Matches(request.downloadHandler.text, "\"Count\":(\\d+)", RegexOptions.Multiline);
             death = int.Parse(matches.Last().Groups[1].Value);
             enemyDeathText.text = death.ToString();
         }
     } */


}
