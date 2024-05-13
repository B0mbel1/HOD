using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class DoorInteraction : NetworkBehaviour
{
    public GameObject door;
    public AudioClip doorSound;
    public AudioSource doorSource;

    private Animator doorAnimator;
    public bool isOpened;
    public bool czyHost;

    void Start()
    {
        doorSource = GetComponent<AudioSource>();
        doorAnimator = door.GetComponent<Animator>();
        isOpened = false;
    }

    void Update()
    {
        if (!IsOwner) return;
    }

    private void OnTriggerStay(Collider other)
    {
        //if (!IsOwner) return;
       
        if (IsHost)
        {
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                OpenDoorClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                OpenDoorServerRpc();
                SynchroServerRpc(1);
            }
        }
        if (!IsHost && IsClient == true)
        {
            if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
            {
                OpenDoorServerRpc();
                SynchroServerRpc(0);
            }
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }

    [ServerRpc(RequireOwnership = false)]
    public void SynchroServerRpc(ulong player)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { player }
            }
        };
        OpenDoorClientRpc(clientRpcParams);
    }

    [ClientRpc]
    public void OpenDoorClientRpc(ClientRpcParams clientRpcParams)
    {
        SezamieOtworzSie();
    }

    [ServerRpc]
    public void OpenDoorServerRpc()
    {
        SezamieOtworzSie();
    }

    public void SezamieOtworzSie()
    {
        if (!isOpened)
        {
            if (doorSound != null && doorSource != null)
            {
                doorSource.PlayOneShot(doorSound);
                doorAnimator.Play("Open Door", 0);
                isOpened = true;
            }
        }
        else
        {
            if (doorSound != null && doorSource != null)
            {
                doorSource.PlayOneShot(doorSound);
                doorAnimator.Play("Close Door", 0);
                isOpened = false;
            }
        }
    }
}


