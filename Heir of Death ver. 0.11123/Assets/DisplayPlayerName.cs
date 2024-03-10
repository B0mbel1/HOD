using TMPro;
using UnityEngine;

public class DisplayPlayerName : MonoBehaviour
{
    public TextMeshProUGUI username;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (PlayfabManager.name != null)
        {
            username.text = PlayfabManager.name;
        }
        else
        {
            username.text = "Goœæ";
        }
    }
}
