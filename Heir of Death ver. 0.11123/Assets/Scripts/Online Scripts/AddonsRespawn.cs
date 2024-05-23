using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AddonsRespawn : NetworkBehaviour
{
    public List<GameObject> remedy, ammo;


    public void FixedUpdate()
    {
        if (RoundHandler.hostDed == true || RoundHandler.klientDed == true)
        {
            if (IsHost)
            {
                Debug.Log("Powinno siê respiæ host");
                OdnowServerRpc();
                SynchroOdnowServerRpc(1);
            }

            if (!IsHost && IsClient)
            {
                Debug.Log("Powinno siê respiæ klient");

                OdnowServerRpc();
                SynchroOdnowServerRpc(1);
                SynchroOdnowServerRpc(0);
                RoundHandler.hostDed = false;
            }
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void OdnowServerRpc()
    {
        foreach (GameObject am in ammo)
        {
            am.SetActive(true);
        }

        foreach (GameObject re in remedy)
        {
            re.SetActive(true);
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void SynchroOdnowServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        OdnowClientRpc(clientRpcParams);
    }

    [ClientRpc]
    public void OdnowClientRpc(ClientRpcParams clientRpcParams)
    {
        foreach (GameObject am in ammo)
        {
            am.SetActive(true);
        }

        foreach (GameObject re in remedy)
        {
            re.SetActive(true);
        }
    }
}
