
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;

using UnityEngine;
using UnityEngine.UI;

public class InvisibilityPowerOnline : NetworkBehaviour
{
    private SkinnedMeshRenderer playerRenderer;
    public Text PowerTimer;

    public float PowerTime;
    public float StartingValue;
    public float CooldownTime;
    public float CooldownStartingValue;
    public float UnlockTime;
    //public bool localBool;
    public bool isActivated;
    public bool CanPressAbility = true;

    public SkinnedMeshRenderer rendererLocal;

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

          
            rendererLocal = GetComponentInChildren<SkinnedMeshRenderer>();

            IsTimerShown(false);
            IsPowerActivated(false);
            isActivated = false;

        }

    }
    //private void Update()
    //{
    //    if (!IsOwner) return;

    //    //if (Input.GetKeyDown(KeyCode.O))
    //    //{
    //    //    Test();
    //    //}
    //    //if (Input.GetKeyDown(KeyCode.P))
    //    //{
    //    //    Test2();
    //    //}

    //}

    private void FixedUpdate()
    {
       
        //if (!IsOwner) return;

        if (Main.Power == 2)
        {
            if (CanPressAbility && Input.GetKeyDown(KeyCode.Q))
            {
                IsPowerActivated(true);
                CanPressAbility = false;
                UnlockTime = (Time.time + 40f) + PowerTime; //Zmieniæ 40 je¿eli chcemy inny cooldown


                if (IsHost)
                {
                    audioSource.PlayOneShot(invisibilitySound);
                    SynchroDzwiekServerRpc(1);
                }

                if (!IsHost && IsClient)
                {
                    audioSource.PlayOneShot(invisibilitySound);
                    SynchroDzwiekServerRpc(0);
                }
                //audioSource.Play();
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

    [ServerRpc]
    public void HostPowerServerRpc(bool show,bool active)
    {
        Power(show, active);
    }

    [ServerRpc]
    public void SynchroPowerServerRpc(bool show, bool active, ulong player)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { player }
            }
        };

        ClientPowerCLientRpc( show, active, clientRpcParams);
    }

    [ClientRpc]
    public void ClientPowerCLientRpc(bool show, bool active, ClientRpcParams clientRpcParams)
    {
        Power(show, active);
    }

    public void Power(bool show,bool active)
    {
        playerRenderer = GetComponentInParent<SkinnedMeshRenderer>();
        playerRenderer.enabled = show;
        isActivated = active;
    }

    public void IsPowerActivated(bool activation)
    {
        if (IsHost)
        {
            if (activation)
            {
                HostPowerServerRpc(false, true);
                SynchroPowerServerRpc(false, true, 1);
                //ClientPowerCLientRpc(false,true, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                isActivated = true;

            }
            else
            {
                HostPowerServerRpc(true, false);
                SynchroPowerServerRpc(true, false, 1);
                //ClientPowerCLientRpc(true, false, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                isActivated = false;
            }
        }
        if (IsClient && !IsHost)
        {
            if (activation)
            {
                //HostPowerServerRpc(false, true);
                SynchroPowerServerRpc(false, true, 0);
                //ClientPowerCLientRpc(true, false, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 0 } } });

                isActivated = true;
            }
            if(activation == false)
            {
                //HostPowerServerRpc(true, false);
                SynchroPowerServerRpc(true, false, 0);
                //ClientPowerCLientRpc(true, false, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 0 } } });

                isActivated = false;
            }
        }
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

    [ClientRpc]
    public void dzwiekINVClientRpc(ClientRpcParams clientRpcParams)
    {
        audioSource.PlayOneShot(invisibilitySound);
        audioSource.volume = 0.4f;
    }

    [ServerRpc(RequireOwnership = false)]
    private void SynchroDzwiekServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        dzwiekINVClientRpc(clientRpcParams);
    }

    //void IsPowerActivated(bool activation)
    //{

    //    if (activation == true)
    //    {
    //        if (IsHost)
    //        {
    //            isActivated = true;
    //            rendererLocal.enabled = false;
    //            UnseeClientRpc(0);
    //            NetworkClient client = NetworkManager.LocalClient;
    //            SendFalseServerRpc(client.ClientId);
    //        }

    //        if(!IsHost && IsClient)
    //        {
    //            isActivated = true;
    //            rendererLocal.enabled = false;
    //            UnseeClientRpc(1);
    //            NetworkClient client = NetworkManager.LocalClient;
    //            SendFalseServerRpc(client.ClientId);
    //        }
    //    }
    //    else
    //    {
    //        if (IsHost) 
    //        {
    //            isActivated = false;
    //            rendererLocal.enabled = true;
    //            SeeClientRpc(0);
    //            NetworkClient client = NetworkManager.LocalClient;
    //            SendTrueServerRpc(client.ClientId);
    //        }

    //        if (!IsHost && IsClient)
    //        {
    //            isActivated = false;
    //            rendererLocal.enabled = true;
    //            SeeClientRpc(1);
    //            NetworkClient client = NetworkManager.LocalClient;
    //            SendTrueServerRpc(client.ClientId);
    //        }

    //    }
    //}






    //[ServerRpc]
    //public void SendFalseServerRpc(ulong client)
    //{
    //    foreach (NetworkClient klient in NetworkManager.ConnectedClientsList)
    //    {
    //        if (klient.ClientId == client)
    //        {
    //            var renderer = klient.PlayerObject.GetComponentInChildren<SkinnedMeshRenderer>();
    //            renderer.enabled = false;
    //            Debug.Log("Na serwerze ustawiony jest niewidzialnosc dla klienta nr" + client);
    //        }
    //    }
    //    UnseeClientRpc(client);
    //}

    //[ServerRpc]
    //public void SendTrueServerRpc(ulong client)
    //{
    //    foreach (NetworkClient klient in NetworkManager.ConnectedClientsList)
    //    {
    //        if (klient.ClientId == client)
    //        {
    //            var renderer = klient.PlayerObject.GetComponentInChildren<SkinnedMeshRenderer>();
    //            renderer.enabled = false;
    //            Debug.Log("Na serwerze ustawiony jest widzialnosc dla klienta nr" + client);
    //        }
    //    }
    //    SeeClientRpc(client);
    //}

    //[ClientRpc]
    //public void UnseeClientRpc(ulong client)
    //{
    //    foreach (NetworkClient klient in NetworkManager.ConnectedClientsList)
    //    {
    //        if (klient.ClientId == client)
    //        {
    //            klient.PlayerObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    //            Debug.Log("aktualizacja niewidzialnosci na kliencie nr" + client);
    //        }
    //    }
    //}

    //[ClientRpc]
    //public void SeeClientRpc(ulong client)
    //{
    //    foreach (NetworkClient klient in NetworkManager.ConnectedClientsList)
    //    {
    //        if (klient.ClientId == client)
    //        {
    //            klient.PlayerObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
    //            Debug.Log("aktualizacja widzialnosci na kliencie nr" + client);
    //        }
    //    }
    //}






    //void InvisibilityOn()
    //{
    //    playerRenderer.enabled = false;

    //}
    //void InvisibilityOff()
    //{
    //    playerRenderer.enabled = true;

    //}

    //void Test()
    //{

    //    NetworkClient client = NetworkManager.LocalClient;
    //    Debug.Log(client.ClientId + " Nr wysyla ze staje sie niewidzalny na serwer");
    //    SendFalseServerRpc(client.ClientId);

    //}

    //void Test2()
    //{

    //    NetworkClient client = NetworkManager.LocalClient;
    //    Debug.Log(client.ClientId + " Nr wysyla ze staje sie widzalny na serwer");
    //    SendTrueServerRpc(client.ClientId);

    //}
}

