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
            Debug.Log("Git");

            // SprawdŸ czy obiekt, z którym kula siê zderza, ma komponent zadaj¹cy obra¿enia
            obrazeniaOK obrazeniaComponent = other.GetComponent<obrazeniaOK>();


            // Jeœli komponent istnieje, zadaj obra¿enia
            if (obrazeniaComponent != null)
            {
                if (BetterAmmoScript.isBetterAmmoActivated == true)
                {
                    obrazeniaComponent.zadajObrazenia(damage + 3);

                }
                else
                {
                    PlayerHealth.TakeDamage(10f);
                    //obrazeniaComponent.zadajObrazenia(damage);
                }


            }
        }

        //if (other.CompareTag("Reka"))
        //{
            
        //}
    }

    public void ZadawaneObrazenia()
    {

    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void Update()
    {
        Invoke("DestroyBullet", 2);
    }
}
