using Unity.Netcode;
using UnityEngine;

public class AmmoScript : NetworkBehaviour
{
    [ServerRpc]
    public void GetAmmoServerRpc()
    {
        BetterAmmoScript.isBetterAmmoPicked = true;
        BetterAmmoScript.TimerActivation = true;
    }

    [ClientRpc]
    public void GetAmmoClientRpc(ClientRpcParams clientRpcParams)
    {
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
    }
}
