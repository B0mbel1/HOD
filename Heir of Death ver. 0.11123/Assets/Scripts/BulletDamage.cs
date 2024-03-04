using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        obrazeniaOK obrazeniaComponent = other.GetComponent<obrazeniaOK>();

        //if (obrazeniaComponent != null)
        //{
        //   
        //}

        if (other.gameObject.tag == "Reka")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                PlayerHealth.TakeDamage(15);
                DestroyBullet();
            }
            if(BetterAmmoScript.isBetterAmmoActivated == false)
            {
                PlayerHealth.TakeDamage(12);
                DestroyBullet();
            }
        }

        if (other.gameObject.tag == "Glowa")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                PlayerHealth.TakeDamage(33);
                DestroyBullet();
            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                PlayerHealth.TakeDamage(30);
                DestroyBullet();
            }
        }

        if (other.gameObject.tag == "Tors")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                PlayerHealth.TakeDamage(23);
                DestroyBullet();
            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                PlayerHealth.TakeDamage(20);
                DestroyBullet();
            }
        }

        if (other.gameObject.tag == "Noga")
        {
            if (BetterAmmoScript.isBetterAmmoActivated == true)
            {
                PlayerHealth.TakeDamage(18);
                DestroyBullet();
            }
            if (BetterAmmoScript.isBetterAmmoActivated == false)
            {
                PlayerHealth.TakeDamage(15);
                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        //Invoke("DestroyBullet", 2);
    }
}
