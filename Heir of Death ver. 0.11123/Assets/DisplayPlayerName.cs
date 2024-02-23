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
        username.text = PlayfabManager.name;
    }
}
