using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class BulletDamage : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //NetworkObject obrazeniaComponent = other.GetComponent<NetworkObject>();
        PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();


        if (other.gameObject.tag == "Reka")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                //PlayerHealth.TakeDamage(15);
                //DestroyBullet();

            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                playerHealth.CmdTakeDamageServerRpc(12);
                //DestroyBullet();
                DestroyBulletServerRpc();
            }
        }

        if (other.gameObject.tag == "Glowa")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                //PlayerHealth.TakeDamage(33);
                //DestroyBullet();

            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                playerHealth.CmdTakeDamageServerRpc(30);
                //DestroyBullet();
                DestroyBulletServerRpc();
            }
        }

        if (other.gameObject.tag == "Tors")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                // PlayerHealth.TakeDamage(23);
                //DestroyBullet();
            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                playerHealth.CmdTakeDamageServerRpc(20);
                //DestroyBullet();
                DestroyBulletServerRpc();

            }
        }

        if (other.gameObject.tag == "Noga")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                //PlayerHealth.TakeDamage(18);
                //DestroyBullet();
            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                playerHealth.CmdTakeDamageServerRpc(15);
                //DestroyBullet();
                DestroyBulletServerRpc();
            }
        }

    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    [ServerRpc (RequireOwnership =false)]
    private void DestroyBulletServerRpc()
    {
        DestroyBulletClientRpc(); // Wywo³aj zdaln¹ procedurê wywo³ywan¹ na klientach do zniszczenia kuli
    }

    [ClientRpc]
    private void DestroyBulletClientRpc()
    {
        //Invoke("Destroy(gameObject)",0.1f); // Zniszcz kulkê na wszystkich klientach

        Destroy(gameObject);
    }


    public void Update()
    {
        if (!IsOwner) { return; }

        DestroyBulletServerRpc();
    }
}
