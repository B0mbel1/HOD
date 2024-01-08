using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineFeaturesHandler : MonoBehaviour
{
    public Button Multi;
    public Button Ranking;
    public TextMeshProUGUI Info;
    public GameObject Logowanie;
    public GameObject Wyloguj;
    // Start is called before the first frame update
    private void Update()
    {
        if (PlayfabManager.name == null)
        {
            Multi.interactable = false;
            Ranking.interactable = false;
            
            DisplayInfoAboutLogin();
            Logowanie.SetActive(true);
            Wyloguj.SetActive(false);
        }
        else
        {
            Logowanie.SetActive(false);
            Wyloguj.SetActive(true);
        }
    }

    public void DisplayInfoAboutLogin()
    {
        Info.text = "Aby skorzystaæ z tych funkcji zaloguj siê";
    }

    public void GotoLogowanie()
    {
        SceneManager.LoadScene("WelcomeScreen");
        
    }
    public void GotoNewLogowanie()
    {
        PlayfabManager.name = null;
        SceneManager.LoadScene("WelcomeScreen");
    }
}
