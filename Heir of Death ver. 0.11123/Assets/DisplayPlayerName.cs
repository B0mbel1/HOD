using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPlayerName : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI username;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        username.text = PlayfabManager.name;
    }

    
}
