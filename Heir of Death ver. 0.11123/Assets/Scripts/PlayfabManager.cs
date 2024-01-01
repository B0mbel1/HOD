using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    public GameObject messageText;
    public GameObject usernameInput;
    public GameObject emailInput;
    public GameObject passwordInput;
    public GameObject emailLogin;
    public GameObject passwordLogin;
    public GameObject messageTextL;
    public GameObject emailInputR;
    public GameObject messageTextR;
    public GameObject LoginScreen;
    public GameObject ResetScreen;
    public GameObject RegisterScreen;
    // Start is called before the first frame update
    void Start()
    {
        //Login();
    }

    public void RegisterButton()
    {
        if(passwordInput.GetComponent<TMP_InputField>().text.Length > 6)
        {
            messageText.GetComponent<TMP_Text>().text = "Has³o za krótkie";
            return;
        }

        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.GetComponent<TMP_InputField>().text,
            Password = passwordInput.GetComponent<TMP_InputField>().text,
            Username = usernameInput.GetComponent<TMP_InputField>().text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
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
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        //messageTextL.GetComponent<TMP_Text>().text = "Zalogowano";
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

    // Update is called once per frame
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
