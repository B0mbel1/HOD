using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BulletDamage : NetworkBehaviour
{
   
    //void OnTriggerEnter(Collider other)
    //{
    //    PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();

    //    if (other.gameObject.CompareTag("Reka"))
    //    {
    //        if (BetterAmmoScript.isBetterAmmoActivated == true)
    //        {


    //        }
    //        if (BetterAmmoScript.isBetterAmmoActivated == false)
    //        {
    //            //playerHealth.CmdTakeDamageServerRpc(12);
    //            playerHealth.DamageServerRpc(12f);
    //            Destroy(other);
    //        }
    //    }

    //    if (other.gameObject.tag == "Glowa")
    //    {
    //        if (BetterAmmoScript.isBetterAmmoActivated == true)
    //        {


    //        }
    //        if (BetterAmmoScript.isBetterAmmoActivated == false)
    //        {
    //            // playerHealth.CmdTakeDamageServerRpc(30);
    //        }
    //    }

    //    if (other.gameObject.tag == "Tors")
    //    {
    //        if (BetterAmmoScript.isBetterAmmoActivated == true)
    //        {


    //        }
    //        if (BetterAmmoScript.isBetterAmmoActivated == false)
    //        {
    //            // playerHealth.CmdTakeDamageServerRpc(20);
    //        }
    //    }

    //    if (other.gameObject.tag == "Noga")
    //    {
    //        if (BetterAmmoScript.isBetterAmmoActivated == true)
    //        {

    //        }
    //        if (BetterAmmoScript.isBetterAmmoActivated == false)
    //        {
    //            //playerHealth.CmdTakeDamageServerRpc(15);
    //        }
    //    }
    //}
}
