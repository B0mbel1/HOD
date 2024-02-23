using Unity.Netcode;
using UnityEngine;

public class OnlineGame : MonoBehaviour
{
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
