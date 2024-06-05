using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static float Power = 0;
    public static float Character = 1;
    public static float gamemode = 0;
    public static int isHost = 0;

    public void PlayGameLas()
    {
        SceneManager.LoadScene("Forest");
    }

    public void PlayGameOkopy()
    {
        SceneManager.LoadScene("Trenches");
    }

    public void PlayGameTest()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void PlayOnlineTest()
    {
        SceneManager.LoadScene("TestSceneOnline");
    }

    public void SetHost()
    {
        isHost = 1;
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

    public void SetPlayerName()
    {

    }

    public void SetChar1()
    {
        Character = 1;
    }

    public void SetChar2()
    {
        Character = 2;
    }

    public void SetChar3()
    {
        Character = 3;
    }

    public void SetChar4()
    {
        Character = 4;
    }

    public void SetChar5()
    {
        Character = 5;
    }

    public void SetGamemode0()
    {
        gamemode = 0;
    }

    public void SetGamemode1()
    {
        gamemode = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
