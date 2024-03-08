
using Unity.Netcode;

using UnityEngine;
using UnityEngine.UI;

public class InvisibilityPowerOnline : NetworkBehaviour
{
    private Renderer playerRenderer;
    public Text PowerTimer;

    public float PowerTime;
    public float StartingValue;
    public float CooldownTime;
    public float CooldownStartingValue;
    public float UnlockTime;
    //public bool localBool;
    public bool isActivated;
    public bool CanPressAbility = true;

    public Renderer rendererLocal;

    public AudioSource audioSource;
    public AudioClip invisibilitySound;

    private void Start()
    {
        if (!IsOwner) return;
        if (Main.Power == 2)
        {
            audioSource.clip = invisibilitySound;

            UnlockTime = StartingValue;
            StartingValue = PowerTime;
            CooldownStartingValue = CooldownTime;

            rendererLocal = GetComponentInChildren<Renderer>();

            IsTimerShown(false);
            IsPowerActivated(false);
            isActivated = false;

        }

    }
    private void Update()
    {
        if (!IsOwner) return;

        if(Input.GetKeyDown(KeyCode.O))
        {
            Test();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Test2();
        }

    }

    private void FixedUpdate()
    {

        if (!IsOwner) return;

        if (Main.Power == 2)
        {
            if (CanPressAbility && Input.GetKeyDown(KeyCode.Q))
            {
                IsPowerActivated(true);
                CanPressAbility = false;
                UnlockTime = (Time.time + 10f) + PowerTime;
                audioSource.Play();
            }

            if (!CanPressAbility && Time.fixedTime >= UnlockTime)
            {
                CanPressAbility = true;
            }

            if (isActivated)
            {
                if (PowerTime > 0)
                {
                    PowerTime -= Time.fixedDeltaTime;
                    IsPowerActivated(true);
                }
                else if (PowerTime <= 0)
                {
                    PowerTime = StartingValue;
                    IsTimerShown(true);
                    IsPowerActivated(false);
                }
                CooldownTime = CooldownStartingValue;
            }
            else
            {
                CoodlwonCountdown();
            }
        }
    }

    void CoodlwonCountdown()
    {
        CooldownTime -= Time.fixedDeltaTime;
        UpdateTimer(CooldownTime);

        if (CooldownTime <= 0)
        {
            CooldownTime = CooldownStartingValue;
            IsTimerShown(false);
        }
    }

    void IsPowerActivated(bool activation)
    {
        if (activation == true)
        {
            if (IsOwner)
            {

                isActivated = true;
                //rendererLocal.enabled = false;
                //UnseeClientRpc();
                //NetworkClient client = NetworkManager.LocalClient;
                //SendFalseServerRpc(client.ClientId);
                //Debug.Log(client.ClientId + "wysyla ze staje sie niewidzalny na serwer");

            }
        }
        else
        {
            if (IsOwner) {
                isActivated = false;
                //rendererLocal.enabled = true;
                //SeeClientRpc();
                //NetworkClient client = NetworkManager.LocalClient;
                //SendTrueServerRpc(client.ClientId);
                //Debug.Log(client.ClientId + "wysyla ze staje sie widzalny na serwer");
            }
        }
    }

    void Test()
    {
       
        NetworkClient client = NetworkManager.LocalClient;
        Debug.Log(client.ClientId + " Nr wysyla ze staje sie niewidzalny na serwer");
        SendFalseServerRpc(client.ClientId);
        
    }

    void Test2()
    {
        
        NetworkClient client = NetworkManager.LocalClient;
        Debug.Log(client.ClientId + " Nr wysyla ze staje sie widzalny na serwer");
        SendTrueServerRpc(client.ClientId);
        
    }

    bool IsTimerShown(bool timerActive)
    {
        if (timerActive == true)
        {
            return PowerTimer.GetComponent<Text>().enabled = true;
        }
        else
        {
            return PowerTimer.GetComponent<Text>().enabled = false;
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        PowerTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void InvisibilityOn()
    {
        playerRenderer.enabled = false;

    }
    void InvisibilityOff()
    {
        playerRenderer.enabled = true;

    }

    [ServerRpc]
    public void SendFalseServerRpc(ulong client)
    {
        foreach(NetworkClient klient in NetworkManager.ConnectedClientsList)
        {
            if(klient.ClientId == client)
            {
                var renderer = klient.PlayerObject.GetComponentInChildren<Renderer>();
                renderer.enabled = false;
                Debug.Log("Na serwerze ustawiony jest niewidzialnosc dla klienta nr" + client);
            }
        }
        UnseeClientRpc(client);
    }

    [ServerRpc]
    public void SendTrueServerRpc(ulong client)
    {
        foreach (NetworkClient klient in NetworkManager.ConnectedClientsList)
        {
            if (klient.ClientId == client)
            {
                var renderer = klient.PlayerObject.GetComponentInChildren<Renderer>();
                renderer.enabled = false;
                Debug.Log("Na serwerze ustawiony jest widzialnosc dla klienta nr" + client);
            }
        }
        SeeClientRpc(client);
    }

    [ClientRpc]
    public void UnseeClientRpc(ulong client)
    {
        foreach(NetworkClient klient in NetworkManager.ConnectedClientsList)
        {
            if (klient.ClientId == client)
            {
                klient.PlayerObject.GetComponentInChildren<Renderer>().enabled = false;
                Debug.Log("aktualizacja niewidzialnosci na kliencie nr" + client);
            }
        }
    }

    [ClientRpc]
    public void SeeClientRpc(ulong client)
    {
        foreach (NetworkClient klient in NetworkManager.ConnectedClientsList)
        {
            if (klient.ClientId == client)
            {
                klient.PlayerObject.GetComponentInChildren<Renderer>().enabled = true;
                Debug.Log("aktualizacja widzialnosci na kliencie nr" + client);
            }
        }
    }
}

