using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnlineStart : MonoBehaviour
{
    // Start is called before the first frame update
    

    public void StartHost()
    {
        
        ServerManager.Instance.StartHost();
        
    }

    public void StartServer()
    {
        ServerManager.Instance.StartServer();
        
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
