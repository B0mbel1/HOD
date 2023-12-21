using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public static float Power=0;
    public static float Character = 1;
    public static string playerName = "";
    public GameObject inputField;
    
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
        playerName = inputField.GetComponent<TMP_InputField>().text;
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
