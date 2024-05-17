using Unity.Netcode;
using UnityEngine;

public class GraveInteraction : NetworkBehaviour
{
    public GameObject grave;
    public bool czyHost;
    public AudioClip screamSound;
    public AudioSource screamSource;

    void Start()
    {
        screamSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!IsOwner) return;
    }

    private void OnTriggerStay(Collider other)
    {
        Skrypt player = other.GetComponentInParent<Skrypt>();

        if(player != null)
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

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }
}
