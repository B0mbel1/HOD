using Unity.Netcode;
using UnityEngine;

public class ShelterInteraction : NetworkBehaviour
{
    public GameObject interactionText;
    public GameObject shelter;

    public AudioClip lookSound;
    public AudioSource pickupSource;

    void Start()
    {
        interactionText.SetActive(false);
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        Skrypt1 player = other.GetComponentInParent<Skrypt1>();

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
