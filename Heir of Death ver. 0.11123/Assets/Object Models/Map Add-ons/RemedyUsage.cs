using Unity.Netcode;
using UnityEngine;

public class RemedyUsage : NetworkBehaviour
{
    public GameObject remedy;

    void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            if (playerHealth.czyHost && IsServer)
            {
                playerHealth.RestoreHealthServerRpc(10f);
                Destroy(gameObject);
            }

            if (playerHealth.czyHost == false && (!IsServer))
            {
                playerHealth.SynchroServerRpc(10f);
                Destroy(gameObject);
            }
        }
    }
}






