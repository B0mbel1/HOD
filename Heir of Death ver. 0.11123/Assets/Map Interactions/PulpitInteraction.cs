using Unity.Netcode;
using UnityEngine;

public class PulpitInteraction : NetworkBehaviour
{
    public GameObject pulpit;

    public AudioClip amenoSound;
    public AudioSource amenoSource;

    void Start()
    {
        amenoSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsOwner) return;
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                amenoSource.Play();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOwner) return;
        
    }
}
