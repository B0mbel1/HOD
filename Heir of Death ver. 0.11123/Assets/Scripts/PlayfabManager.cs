using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using PlayFab.EconomyModels;

public class PlayfabManager : MonoBehaviour
{
    public GameObject messageText;
    public GameObject usernameInput;
    public GameObject emailInput;
    public GameObject passwordInput;
    public GameObject password2Input;
    public GameObject emailLogin;
    public GameObject passwordLogin;
    public GameObject messageTextL;
    public GameObject emailInputR;
    public GameObject messageTextR;
    public GameObject LoginScreen;
    public GameObject ResetScreen;
    public GameObject RegisterScreen;
    public static string name = null;
    
    

    private void Start()
    {
        
    }
    public class PlayerData
    {
        public int position;
        public string playerName;
        public int score;
    }
    // Start is called before the first frame update

    public void RegisterButton()
    {
        if(passwordInput.GetComponent<TMP_InputField>().text.Length > 6)
        {
            messageText.GetComponent<TMP_Text>().text = "Has³o za krótkie";
            return;
        }
        if (passwordInput.GetComponent<TMP_InputField>().text.Equals(password2Input.GetComponent<TMP_InputField>().text))
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = emailInput.GetComponent<TMP_InputField>().text,
                Password = passwordInput.GetComponent<TMP_InputField>().text,
                DisplayName = usernameInput.GetComponent<TMP_InputField>().text,
                Username = usernameInput.GetComponent<TMP_InputField>().text,
                RequireBothUsernameAndEmail = true
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
        else
        {
            messageText.GetComponent<TMP_Text>().text = "Has³a nie s¹ identyczne";
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageTextL.GetComponent<TMP_Text>().text = "Zarejestrowano";
        RegisterScreen.SetActive(false);
        LoginScreen.SetActive(true);
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLogin.GetComponent<TMP_InputField>().text,
            Password = passwordLogin.GetComponent<TMP_InputField>().text,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams()
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        
        
        name = result.InfoResultPayload.PlayerProfile.DisplayName;
        SceneManager.LoadScene("Menu");
    }

    public void ResetPassword()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInputR.GetComponent<TMP_InputField>().text,
            TitleId = "87196"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageTextL.GetComponent<TMP_Text>().text = "Email wys³ano!";
        ResetScreen.SetActive(false);
        LoginScreen.SetActive(true);
    }

    void OnError(PlayFabError error)
    {
        messageTextL.GetComponent<TMP_Text>().text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    

    

    //
    /*
    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Zalogowano");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("B³¹d logowania");
        Debug.Log(error.GenerateErrorReport());
    }
    */
}
