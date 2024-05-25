using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance { get; private set; }
    public Dictionary<ulong, ClientData> ClientData { get; private set; }
    private bool gameHasStarted;

    [SerializeField] public List<string> SceneList = new List<string>();
    [SerializeField] private string gameplaySceneName = "";
    [SerializeField] private string characterSelectSceneName = "SelectCharacter";
    [SerializeField] InputField ip;
    [SerializeField] string ipAddress;
    [SerializeField] UnityTransport transport;

    public static string AdresIP;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartServer()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.OnServerStarted += OnNetworkReady;
        ClientData = new Dictionary<ulong, ClientData>();
        NetworkManager.Singleton.StartServer();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        NetworkManager.Singleton.OnServerStarted += OnNetworkReady;
        ClientData = new Dictionary<ulong, ClientData>();
        NetworkManager.Singleton.StartHost();
        GetLocalIPAddress();
    }


    public void StartClient()
    {
        //NetworkManager.Singleton.ConnectionApprovalCallback += ApprovalCheck;
        ipAddress = ip.text;
        SetIpAddress(ipAddress);
        
        NetworkManager.Singleton.StartClient();
    }

    public void StopServer()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.ConnectionApprovalCallback -= ApprovalCheck;
            NetworkManager.Singleton.OnServerStarted -= OnNetworkReady;
        }

        NetworkManager.Singleton.Shutdown();
        NetworkManager networkManager = GameObject.FindObjectOfType<NetworkManager>();
        Destroy(networkManager.gameObject);
        Destroy(gameObject);
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        if (ClientData.Count >= 4 || gameHasStarted)
        {
            response.Approved = false;
            return;
        }

        response.Approved = true;
        response.CreatePlayerObject = false;
        response.Pending = false;

        ClientData[request.ClientNetworkId] = new ClientData(request.ClientNetworkId);

        Debug.Log("Added client " + request.ClientNetworkId);
        Debug.Log("IP:" + GetLocalIPAddress());
    }

    //Wyœwietla IP hosta
    public string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                AdresIP = ip.ToString();
                ipAddress = ip.ToString();
                return ip.ToString();
            }
        }

        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    //Ustawia IP clienta
    public void SetIpAddress(string ipAddress)
    {
        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = ipAddress;
    }




    private void OnNetworkReady()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
        NetworkManager.Singleton.SceneManager.LoadScene(characterSelectSceneName, LoadSceneMode.Single);
    }

    private void OnClientDisconnect(ulong clientId)
    {
        if (ClientData.ContainsKey(clientId))
        {
            if (ClientData.Remove(clientId))
            {
                Debug.Log("Removed client " + clientId);
            }
        }
    }

    public void SetCharacter(ulong clientId, int characterId)
    {
        if (ClientData.TryGetValue(clientId, out ClientData data))
        {
            data.characterId = characterId;
        }
    }

    public int randomMap()
    {
        return Random.Range(0, 2);
    }

    public void StartGame()
    {
        gameHasStarted = true;
        gameplaySceneName = SceneList[randomMap()];
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }
}
