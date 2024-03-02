using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public int damage = 10; // Moc obra¿eñ zadawanych przez kulê

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Reka")
        {
            Debug.Log("Reka");
            obrazeniaOK obrazeniaComponent = other.GetComponent<obrazeniaOK>();

            if (obrazeniaComponent != null)
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    obrazeniaComponent.zadajObrazenia(damage + 3);
                    DestroyBullet();
                }
                else
                {
                    PlayerHealth.TakeDamage(10f);
                    DestroyBullet();
                    //obrazeniaComponent.zadajObrazenia(damage);
                }
            }
        }

        if (other.gameObject.tag == "Glowa")
        {
            Debug.Log("HEDSZOT");

            obrazeniaOK obrazeniaComponent = other.GetComponent<obrazeniaOK>();
  
            if (obrazeniaComponent != null)
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    obrazeniaComponent.zadajObrazenia(damage + 3);
                    DestroyBullet();

                }
                else
                {
                    PlayerHealth.TakeDamage(50f);
                    //obrazeniaComponent.zadajObrazenia(damage);
                    DestroyBullet();
                }
            }
        }

        if (other.gameObject.tag == "Tors")
        {
            Debug.Log("Bêben");

            obrazeniaOK obrazeniaComponent = other.GetComponent<obrazeniaOK>();

            if (obrazeniaComponent != null)
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    obrazeniaComponent.zadajObrazenia(damage + 3);
                    DestroyBullet();
                }
                else
                {
                    PlayerHealth.TakeDamage(50f);
                    DestroyBullet();
                    //obrazeniaComponent.zadajObrazenia(damage);
                }
            }
        }

        if (other.gameObject.tag == "Noga")
        {
            Debug.Log("Gira");

            obrazeniaOK obrazeniaComponent = other.GetComponent<obrazeniaOK>();

            if (obrazeniaComponent != null)
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    obrazeniaComponent.zadajObrazenia(damage + 3);
                    DestroyBullet();
                }
                else
                {
                    PlayerHealth.TakeDamage(50f);
                    DestroyBullet();
                }
            }
        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        Invoke("DestroyBullet", 0.1f);
    }
}
