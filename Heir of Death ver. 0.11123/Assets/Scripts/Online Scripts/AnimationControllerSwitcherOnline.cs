using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AnimationControllerSwitcherOnline : NetworkBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController rifleanimations;
    public RuntimeAnimatorController gunanimations;
    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;

    void LateUpdate()
    {
        if (!IsOwner) { return; }

        if (IsHost)
        {
            if (gun1.activeSelf == true && gun2.activeSelf == false)
            {
                HostRifleServerRpc();
                ClientRifleClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
            }

            if (gun2.activeSelf == true && gun1.activeSelf == false)
            {
                HostPistolServerRpc();
                ClientPistolClientRpc(new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
            }
        }

        if (!IsHost && IsClient)
        {
            if (gun1.activeSelf == true && gun2.activeSelf == false)
            {
                HostRifleServerRpc();
                SynchroRifleServerRpc(1);
            }

            if (gun2.activeSelf == true && gun1.activeSelf == false)
            {
                HostPistolServerRpc();
                SynchroPistolServerRpc(1);
            }
        }
    }

    public void changeAnimation(RuntimeAnimatorController gun)
    {
        animator.runtimeAnimatorController = gun;
    }

    [ServerRpc]
    public void HostRifleServerRpc()
    {
        animator.runtimeAnimatorController = rifleanimations;
    }

    [ServerRpc]
    public void HostPistolServerRpc()
    {
        animator.runtimeAnimatorController = gunanimations;
    }

    [ClientRpc]
    public void ClientRifleClientRpc(ClientRpcParams clientRpcParams)
    {
        animator.runtimeAnimatorController = rifleanimations;
    }

    [ClientRpc]
    public void ClientPistolClientRpc(ClientRpcParams clientRpcParams)
    {
        animator.runtimeAnimatorController = gunanimations;
    }

    [ServerRpc]
    public void SynchroRifleServerRpc(ulong player)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { player }
            }
        };

        ClientRifleClientRpc(clientRpcParams);
    }

    [ServerRpc]
    public void SynchroPistolServerRpc(ulong player)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { player }
            }
        };

        ClientPistolClientRpc(clientRpcParams);
    }
}