using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class OnlineGame : MonoBehaviour
{
    // Start is called before the first frame update
    // Update is called once per frame
    void Start()
    {
        if(Main.isHost==1)
        {
            NetworkManager.Singleton.StartHost();
        }
        if(Main.isHost==0) 
        {
            NetworkManager.Singleton.StartClient();
        }
    }
}
