using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutOnQuit : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        DeleteCurrentId(null);
    }

    public void DeleteCurrentId(string DefaultID)
    {
        var request = new UpdateUserDataRequest
        {

            Data = new Dictionary<string, string>
        {
            { "localDeviceId", DefaultID }
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
}
