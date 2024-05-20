using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

public class MenuPauzyOnline : NetworkBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject Canvas;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Canvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    void Pause()
    {
        Canvas.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Leaderboard tablica = new Leaderboard();
        //tablica.SendLeaderboard(Damaging.wynik);
    }

    public void LoadMenu()
    {
        if (IsHost)
        {
            NetworkManager.Singleton.Shutdown();
            
        }

        if (IsClient)
        {
            NetworkManager.Singleton.DisconnectClient(OwnerClientId);
            
        }
        
        Cleanup();

        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu");
    }

    void Cleanup()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
            Destroy(ServerManager.Instance.gameObject);
        }
    }
}
