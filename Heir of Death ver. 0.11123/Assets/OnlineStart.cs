using UnityEngine;

public class OnlineStart : MonoBehaviour
{
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
        ServerManager.Instance.StartClient();
    }
}
