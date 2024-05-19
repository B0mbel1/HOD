using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ChangeGun : NetworkBehaviour
{
    [SerializeField] public GameObject gun1;
    [SerializeField] public GameObject gun2;

    void Start()
    {
        gun2.SetActive(false);
    }

    void Update()
    {
        if (!IsOwner) return;

        if (MenuPauzyOnline.GameIsPaused == false)
        {
            if (IsHost)
            {
                if (Input.GetKeyDown(KeyCode.Q) && FreeTeleportationOnline.teleportState == true)
                {
                    gun2.SetActive(false);
                    gun1.SetActive(false);
                }
                else
                {
                    if (!Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        HostShowGunServerRpc(true, false);
                        ShowGunClientRpc(true, false, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    }

                    if (!Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        HostShowGunServerRpc(false, true);
                        ShowGunClientRpc(false, true, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    }
                }
            }

            if (!IsHost && IsClient)
            {
                if (Input.GetKeyDown(KeyCode.Q) && FreeTeleportationOnline.teleportState == true)
                {
                    gun2.SetActive(false);
                    gun1.SetActive(false);
                }
                else
                {
                    if (!Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        SynchroShowGunServerRpc(true, false, 1);
                        HostShowGunServerRpc(true, false);
                    }

                    if (!Input.GetKey(KeyCode.Q) && Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        SynchroShowGunServerRpc(false, true, 1);
                        HostShowGunServerRpc(false, true);
                    }
                }
            }
        }
    }

    public void ShowGun(bool show1, bool show2)
    {
        gun1.SetActive(show1);
        gun2.SetActive(show2);
    }

    [ServerRpc]
    public void HostShowGunServerRpc(bool show1, bool show2)
    {
        ShowGun(show1, show2);
    }

    [ClientRpc]
    public void ShowGunClientRpc(bool show1, bool show2, ClientRpcParams clientRpcParams)
    {
        ShowGun(show1, show2);
    }

    [ServerRpc]
    public void SynchroShowGunServerRpc(bool show1, bool show2, ulong player)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { player }
            }
        };

        ShowGunClientRpc(show1, show2, clientRpcParams);
    }
}
