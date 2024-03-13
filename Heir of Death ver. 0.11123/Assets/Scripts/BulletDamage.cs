using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletDamage : NetworkBehaviour
{
    private void Start()
    {
        Destroy(gameObject,2f);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth.czyHost == true)
        {
            if (other.gameObject.tag == "Reka")
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageServerRpc(13f);
                    Destroy(gameObject);

                }

                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageServerRpc(10f);
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Noga")
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageServerRpc(15f);
                    Destroy(gameObject);
                }

                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageServerRpc(12f);
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Glowa")
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageServerRpc(23f);
                    Destroy(gameObject);
                }

                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageServerRpc(20f);
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Tors")
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageServerRpc(18f);
                    Destroy(gameObject);
                }

                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageServerRpc(15f);
                    Destroy(gameObject);
                }
            }
        }

        if (playerHealth.czyHost == false)
        {
            if (other.gameObject.tag == "Reka")
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageClientRpc(13f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }

                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageClientRpc(10f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Noga")
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageClientRpc(15f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageClientRpc(12f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Glowa")
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageClientRpc(20f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }

                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageClientRpc(20f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Tors")
            {

                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    playerHealth.DamageClientRpc(15f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }

                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageClientRpc(15f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
            }
        }
    }
}
