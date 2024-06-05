using Unity.Netcode;
using UnityEngine;

public class PulpitInteraction : NetworkBehaviour
{
    public GameObject pulpit;

    public AudioClip amenoSound;

    public void Update()
    {
        if (!IsOwner) return;
    }

    private void OnTriggerStay(Collider other)
    {
        Skrypt player = other.GetComponentInParent<Skrypt>();

        if (player != null)
        {
            if (player.czyHost == true)
            {
                if (other.gameObject.tag == "Player")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        player.BulletSpawnServerRpc();
                    }
                }
            }

            if (player.czyHost == false)
            {
                if (other.gameObject.tag == "Player")
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        player.SynchroServerRpc();
                    }
                }
            }
        }
    }
}
