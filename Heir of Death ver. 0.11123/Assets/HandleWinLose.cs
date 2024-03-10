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
        //if (!IsOwner) return;
        if (RoundCounter.counterhost == 10)
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
        RoundCounter.counterhost = 0;
        RoundCounter.counterclient = 0;
        Time.timeScale = 1f;
        ServerManager.Instance.StopServer();

        
        SceneManager.LoadScene("Menu");
    }
}
