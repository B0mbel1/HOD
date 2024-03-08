using Unity.Netcode;
using UnityEngine;

public class BulletDamage : NetworkBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInChildren<PlayerHealth>();

        if (other.gameObject.tag == "Reka")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {


            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                //playerHealth.CmdTakeDamageServerRpc(12);
            }
        }

        if (other.gameObject.tag == "Glowa")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                

            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
               // playerHealth.CmdTakeDamageServerRpc(30);
            }
        }

        if (other.gameObject.tag == "Tors")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {


            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
               // playerHealth.CmdTakeDamageServerRpc(20);
            }
        }

        if (other.gameObject.tag == "Noga")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {

            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                //playerHealth.CmdTakeDamageServerRpc(15);
            }
        }
    }
}
