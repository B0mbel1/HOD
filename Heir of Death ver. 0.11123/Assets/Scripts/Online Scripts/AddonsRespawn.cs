using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AddonsRespawn : NetworkBehaviour
{
    public List<GameObject> remedy, ammo;

    public void Update()
    {
        if (!IsOwner)
        {
            if (RoundHandler.odnow == true)
            {
                OdnowServerRpc();
                SynchroOdnowServerRpc(1);
            }
        }

        if (IsOwner)
        {
            if (RoundHandler.odnow == true)
            {
                OdnowServerRpc(); 
                SynchroOdnowServerRpc(1);
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
