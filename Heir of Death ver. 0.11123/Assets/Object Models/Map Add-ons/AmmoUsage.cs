using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PickupScript : NetworkBehaviour
{
    public GameObject pickUptext;
    public GameObject clip;

    public AudioClip pickupSound;
    public AudioSource pickupSource;

    public bool czyHost;

    private void Start()
    {
        //pickUptext.SetActive(false);
        pickupSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!IsOwner) return;

        if (IsHost)
        {
            czyHost = true;
        }
        if (IsClient && !IsHost)
        {
            czyHost = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUptext.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if (IsHost == true && IsOwner)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        if (Input.GetKeyDown(KeyCode.E))
        //        {
        //            pickUptext.SetActive(false);

        //            this.gameObject.SetActive(false);
        //            clip.SetActive(false);
        //            BetterAmmoScript.isBetterAmmoPicked = true;
        //            BetterAmmoScript.TimerActivation = true;

        //            SynchroServerRpc();
        //        }
        //    }

        //}


        //if (IsHost)
        //{
        //    if (other.gameObject.tag == "Player")
        //    {
        //        if (Input.GetKeyDown(KeyCode.E))
        //        {
        //            //SynchroServerRpc();
        //            getHealthClientRpc(false, true, true, true, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
        //        }
        //    }
        //}
        if (IsOwner)
        {

            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //getHealthClientRpc(false, true, true, true, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 0,1 } } });
                   
                    SynchroServerRpc(0);
                }
            }
        }
        else
        {
            if (other.gameObject.tag == "Player")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //getHealthClientRpc(false, true, true, true, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 0,1 } } });
                    
                    SynchroServerRpc(1);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUptext.SetActive(false);
        }
    }

    [ServerRpc]
    public void GetAmmoServerRpc()
    {
        this.gameObject.SetActive(false);
        clip.SetActive(false);
        BetterAmmoScript.isBetterAmmoPicked = true;
        BetterAmmoScript.TimerActivation = true;
    }

    [ClientRpc]
    public void GetAmmoClientRpc( ClientRpcParams clientRpcParams)
    {
        this.gameObject.SetActive(false);
        clip.SetActive(false);
        pickUptext.SetActive(false);
        BetterAmmoScript.isBetterAmmoPicked = true;
        BetterAmmoScript.TimerActivation = true;
    }

    [ServerRpc(RequireOwnership =false)]
    public void SynchroServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        GetAmmoClientRpc(clientRpcParams);
    }
}
