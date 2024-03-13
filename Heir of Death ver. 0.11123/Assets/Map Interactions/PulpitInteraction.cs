using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PulpitInteraction : NetworkBehaviour
{
    public GameObject interactionText;
    public GameObject pulpit;
    public bool czyHost;
    public AudioClip amenoSound;
    public AudioSource amenoSource;
 
    void Start()
    {
        interactionText.SetActive(false);
        amenoSource = GetComponent<AudioSource>();
    }

    public void Update()
    {
        if (!IsOwner) return;
    }

    private void OnTriggerStay(Collider other)
    {
        Skrypt player = other.GetComponentInParent<Skrypt>();
       
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

        if(player.czyHost == false)
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

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
    }
}
