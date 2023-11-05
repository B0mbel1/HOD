using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static float Power=0;
    
    public void PlayGameLas()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlayGameOkopy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void PlayGameTest()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void SetPowerInvisibility()
    {
        Power = 2;
    }

    public void SetPowerXray()
    {
        Power = 3;
    }

    public void SetPowerStealth()
    {
        Power = 4;
    }

    public void SetPowerTeleport()
    {
        Power = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
