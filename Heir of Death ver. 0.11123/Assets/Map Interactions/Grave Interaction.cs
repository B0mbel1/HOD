using Unity.Netcode;
using UnityEngine;

public class GraveInteraction : NetworkBehaviour
{
    public GameObject interactionText;
    public GameObject grave;

    public AudioClip horrorSound;
    public AudioSource pickupSource;

    void Start()
    {
        interactionText.SetActive(false);
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        if (other.gameObject.tag == "Player")
        {
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsOwner) return;
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupSource.Play();
                interactionText.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOwner) return;
        if (other.gameObject.tag == "Player")
        {
            interactionText.SetActive(false);
        }
    }
}
