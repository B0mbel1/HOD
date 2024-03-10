using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviour
{
    public static ServerManager Instance { get; private set; }
    public Dictionary<ulong, ClientData> ClientData { get; private set; }
    private bool gameHasStarted;
    [SerializeField] public List<string> SceneList = new List<string>();

    [SerializeField] private string gameplaySceneName = "";
    [SerializeField] private string characterSelectSceneName = "SelectCharacter";

    private void Awake()
    {
        if(Instance != null && Instance != this)
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
    }

    private void OnNetworkReady()
    {
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnect;
        NetworkManager.Singleton.SceneManager.LoadScene(characterSelectSceneName, LoadSceneMode.Single);
    }

    private void OnClientDisconnect(ulong clientId)
    {
        if(ClientData.ContainsKey(clientId))
        {
            if(ClientData.Remove(clientId))
            {
                Debug.Log("Removed client " + clientId);
            }
        }
    }

    public void SetCharacter(ulong clientId, int characterId)
    {
        if(ClientData.TryGetValue(clientId, out ClientData data))
        {
            data.characterId = characterId;
        }
    }

    public void StartGame()
    {
        gameHasStarted = true;
        gameplaySceneName = SceneList[0];
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    
}
