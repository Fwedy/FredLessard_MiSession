using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using Newtonsoft.Json;
using System.IO;

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

    private string currentUser = null;

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


        if (CheckEmailValidity())
        {
            email = signUpEmailField.text.ToString();
            password = signUpPswdField.text.ToString();
            if (password.Length > 1)
            {
                StartCoroutine(CheckEmailUnique());

            }
            else
            {
                SU_InfoTxt.text = "Some input information is missing.";
                SU_InfoTxt.color = Color.red;
            }
        }
    }

    public void OnSignIn()
    {
        password = signInPswdField.text.ToString();

        if (CheckEmailValidity())
        {


            if (password.Length > 1)
            {

                email = signInEmailField.text.ToString();

                StartCoroutine(CheckCorrectPassword());
            }
            else
            {
                SI_InfoTxt.text = "Some input information is missing.";
                SI_InfoTxt.color = Color.red;
            }
        }

    }

    public void OnResendEmailVerification()
    {

        if (CheckEmailValidity())
        {
            SI_InfoTxt.text = "";
            email = signUpEmailField.text.ToString();

            StartCoroutine(EmailVerification());

        }

    }
    private bool CheckEmailValidity()
    {
        //only one "@"
        //after @, XXXX"."xxx
        string emailPattern = @"^[a-z][^@ ]+@\w+\.\w+";

        if (Regex.IsMatch(signInEmailField.text.ToString(), emailPattern, RegexOptions.IgnoreCase) ||
           Regex.IsMatch(signUpEmailField.text.ToString(), emailPattern, RegexOptions.IgnoreCase))
        {

            return true;
        }
        else
        {
            SI_InfoTxt.text = "Email format is incorect.";
            SI_InfoTxt.color = Color.red;
            SU_InfoTxt.text = "Email format is incorect.";
            SU_InfoTxt.color = Color.red;
            return false;
        }

    }

    public IEnumerator CheckEmailUnique()
    {

        using (var request = new WebRequestBuilder().SetUrl("classes/_User/?where={\"username\":\"" + email + "\"}")
        .SetType("GET")
        .SetDownloadHandler(new DownloadHandlerBuffer())
        .Build())
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error:" + request.error);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);



            if (request.downloadHandler.text.Length <= 15)
            {

                StartCoroutine(CreateNewAccount());
                yield break;

            }
            else
            {
                SU_InfoTxt.text = "This email is already in use";
                SU_InfoTxt.color = Color.red;
                yield break;
            }
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


            var json = JsonConvert.SerializeObject(new { password = password, email = email, username = email });


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
            var json = JsonConvert.SerializeObject(new { email = email });

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

    public IEnumerator CheckCorrectPassword()
    {
        using (var request = new WebRequestBuilder().SetUrl("classes/_User/?where={\"username\":\"" + email + "\"}")
        .SetType("GET")
        .SetDownloadHandler(new DownloadHandlerBuffer())
        .Build())
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error:" + request.error);
                yield break;
            }
            Debug.Log(request.downloadHandler.text);



            if (request.downloadHandler.text.Length <= 15)
            {

                SU_InfoTxt.text = "This user does not exist.";
                SU_InfoTxt.color = Color.red;
                yield break;

            }
            else
            {
                StartCoroutine(SignIn());


                yield break;
            }
        }

    }

    public IEnumerator SignIn()
    {



        using (var request = new WebRequestBuilder()
        .SetUrl("login")
        .SetType("POST")
        .Revocable()
        .SetJSON(new { username = email, password = password })
        .SetDownloadHandler(new DownloadHandlerBuffer())
        .Build())
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                SI_InfoTxt.text = "Password is incorect.";
                SI_InfoTxt.color = Color.red;
                yield break;
            }
            currentUser = email;
            SI_InfoTxt.text = "Signed In Successfully";
            SI_InfoTxt.color = Color.green;
            Debug.LogWarning(request.downloadHandler.text);
            //StartCoroutine(BackupSaveData());
        }


    }

    public IEnumerator BackupSaveData()
    {

        using (var request = new WebRequestBuilder().SetUrl("classes/SaveData")
        .SetType("POST")
        .SetDownloadHandler(new DownloadHandlerBuffer())
        .JSONContentType()
        .SetJSON(new { username = email, save_data = Path.Combine(Application.persistentDataPath, "Save.data") })
        .Build())
        {
            yield return request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(request.downloadHandler.text);
                yield break;
            }
        }

    }


    public void EmailFieldValidator_SU()
    {
        if (CheckEmailValidity())
        {
            signUpEmailField.color = Color.black;
            SU_InfoTxt.text = "";

        }
        else
        {
            signUpEmailField.color = Color.red;
        }
    }

    public void EmailFieldValidator_SI()
    {
        if (CheckEmailValidity())
        {
            signInEmailField.color = Color.black;
            SI_InfoTxt.text = "";
        }
        else
        {
            signInEmailField.color = Color.red;
        }
    }


}
