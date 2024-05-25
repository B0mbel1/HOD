using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    public GameObject usernameInput;
    public GameObject emailInput;
    public GameObject passwordInput;
    public GameObject password2Input;
    public GameObject emailLogin;
    public GameObject passwordLogin;
    public GameObject messageTextL;
    public GameObject emailInputR;
    public GameObject messageTextR;
    public GameObject messageTextP;
    public GameObject LoginScreen;
    public GameObject ResetScreen;
    public GameObject RegisterScreen;
    public static string name = null;
    public static string MyPlayfabID;
    public static string gottenId;
    
    public class PlayerData
    {
        public int position;
        public string playerName;
        public int score;
    }

    

    public void RegisterButton()
    {
        if (passwordInput.GetComponent<TMP_InputField>().text.Length > 6)
        {
            messageTextR.GetComponent<TMP_Text>().text = "Has³o za krótkie";
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
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnErrorR);
        }
        else
        {
            messageTextR.GetComponent<TMP_Text>().text = "Has³a nie s¹ identyczne";
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
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnErrorL);
        
    }

    void OnLoginSuccess(LoginResult result)
    {
        GetAccountInfo();
        GetLocalDeviceId();
        name = result.InfoResultPayload.PlayerProfile.DisplayName;
    }

    public void SaveLocalDeviceId(string localDeviceId)
    {
        var request = new UpdateUserDataRequest
        {

            Data = new Dictionary<string, string>
        {
            { "localDeviceId", localDeviceId }
        }
        };

        PlayFabClientAPI.UpdateUserData(request, result =>
        {
            Debug.Log("Local device ID saved successfully!");
        }, error =>
        {
            Debug.LogError($"Error saving local device ID: {error.ErrorMessage}");
        });
    }
    public void GetLocalDeviceId() {
        var request = new GetUserDataRequest
        {
            PlayFabId = MyPlayfabID,
            Keys = new List<string> { "localDeviceId" } // Klucz dla zmiennej poziomu
        };

        // Wywo³aj ¿¹danie
        PlayFabClientAPI.GetUserData(request, OnGetUserDataSuccess, OnGetUserDataError);
        
    }

private void OnGetUserDataSuccess(GetUserDataResult result)
{
    if (result.Data.TryGetValue("localDeviceId", out var idValue))
    {
        string id = idValue.Value;
        gottenId = idValue.Value;
        Debug.Log($"ID: " +id);
    }
    
        if (gottenId == null)
        {
            SaveLocalDeviceId(SystemInfo.deviceUniqueIdentifier);
            
            SceneManager.LoadScene("Menu");
        }
        else
        {
            
            messageTextL.GetComponent<TMP_Text>().text = "U¿ytkownik jest zalogowany na to konto";
            CleanLogin();
        }
    }

    private void CleanLogin()
    {
        name = null;
        gottenId = null;
        MyPlayfabID = null;
        emailLogin.GetComponent<TMP_InputField>().text = null;
        passwordLogin.GetComponent<TMP_InputField>().text = null;
        PlayFabClientAPI.ForgetAllCredentials();
    }
private void OnGetUserDataError(PlayFabError error)
{
    Debug.LogError($"B³¹d pobierania danych gracza: {error.ErrorMessage}");
}

    void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request, Successs, fail);
    }


    void Successs(GetAccountInfoResult result)
    {

        MyPlayfabID = result.AccountInfo.PlayFabId;

    }


    void fail(PlayFabError error)
    {

        Debug.LogError(error.GenerateErrorReport());
    }

    
    

    public void ResetPassword()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInputR.GetComponent<TMP_InputField>().text,
            TitleId = "87196"
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnErrorP);
    }

    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        messageTextL.GetComponent<TMP_Text>().text = "Email wys³ano!";
        ResetScreen.SetActive(false);
        LoginScreen.SetActive(true);
    }

    void OnErrorL(PlayFabError error)
    {
        messageTextL.GetComponent<TMP_Text>().text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void OnErrorR(PlayFabError error)
    {
        messageTextR.GetComponent<TMP_Text>().text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    void OnErrorP(PlayFabError error)
    {
        messageTextP.GetComponent<TMP_Text>().text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
}
