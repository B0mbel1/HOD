using TMPro;
using Unity.Netcode;
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
        Cleanup();
        SceneManager.LoadScene("WelcomeScreen");
    }

    public void GotoNewLogowanie()
    {
        Cleanup();
        PlayfabManager.name = null;
        SceneManager.LoadScene("WelcomeScreen");
    }

    void Cleanup()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }
}
