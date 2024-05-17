using Unity.Netcode;
using UnityEngine;

public class AmmoUsage : NetworkBehaviour
{
    public GameObject clip;

    public bool czyHost;

    void OnTriggerEnter(Collider other)
    {
        AmmoScript ammoScript = other.GetComponentInParent<AmmoScript>();
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
        if (playerHealth != null)
        {
            if (playerHealth.czyHost && IsServer) 
            {
                Debug.Log("Host wzia³");
                ammoScript.GetAmmoServerRpc();
                Destroy(gameObject);
            }

            if (playerHealth.czyHost == false && (!IsServer))
            {
                Debug.Log("Client wzia³");
                ammoScript.SynchroServerRpc();
                Destroy(gameObject);
            }
        }
    }
}
