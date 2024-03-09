using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletDamage : NetworkBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth.czyHost == true)
        {
            if (other.gameObject.tag == "Reka")
            {

                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {


                }
                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageServerRpc(12f);
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Noga")
            {

                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {


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


                }
                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageServerRpc(14f);
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Tors")
            {

                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {


                }
                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageServerRpc(18f);
                    Destroy(gameObject);
                }
            }
            if (other.gameObject.tag == "Untagged")
            {
                Destroy(gameObject);
            }
        }



        if (playerHealth.czyHost == false)
        {
            if (other.gameObject.tag == "Reka")
            {

                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    

                }
                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageClientRpc(12f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Noga")
            {

                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {


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


                }
                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageClientRpc(12f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
            }

            if (other.gameObject.tag == "Tors")
            {

                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {


                }
                if (BetterAmmoScript.isBetterAmmoActivated == false)
                {
                    playerHealth.DamageClientRpc(12f, new ClientRpcParams { Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } } });
                    Destroy(gameObject);
                }
            }

            if(other.gameObject.tag == "Untagged")
            {
                Destroy(gameObject);
            }
        }
    }
}
