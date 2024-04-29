using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AmmoScript : NetworkBehaviour
{
    [ServerRpc]
    public void GetAmmoServerRpc()
    {
        //this.gameObject.SetActive(false);
        BetterAmmoScript.isBetterAmmoPicked = true;
        BetterAmmoScript.TimerActivation = true;
    }

    [ClientRpc]
    public void GetAmmoClientRpc(ClientRpcParams clientRpcParams)
    {
        //this.gameObject.SetActive(false);
        BetterAmmoScript.isBetterAmmoPicked = true;
        BetterAmmoScript.TimerActivation = true;
    }

    [ServerRpc]
    public void SynchroServerRpc()
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { 1 }
            }
        };

        GetAmmoClientRpc(clientRpcParams);
        Debug.Log("Powinno siê wykonaæ synchro");
    }
}
