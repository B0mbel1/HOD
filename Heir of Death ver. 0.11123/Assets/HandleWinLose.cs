using PlayFab.EventsModels;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleWinLose : NetworkBehaviour
{
    public GameObject winScreen;
    public GameObject loseScreen;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            ActivateWinScreen();
        }
        if(Input.GetKeyDown(KeyCode.RightControl))
        {
            ActivateLoseScreen();
        }
    }

    public override void OnNetworkDespawn()
    {

        base.OnNetworkDespawn();
    }
    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ActivateLoseScreen()
    {
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Zagrajjeszczeraz()
    {

    }

    

    public void WyjdzZGry()
    {
        ServerManager.Instance.StopServer();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
