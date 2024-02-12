using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulpitInteraction : MonoBehaviour
{
    public GameObject interactionText;
    public GameObject pulpit;

    public AudioClip amenoSound;
    public AudioSource pickupSource;


    void Start()
    {
        interactionText.SetActive(false);
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactionText.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
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
        if (other.gameObject.tag == "Player")
        {
            interactionText.SetActive(false);
        }
    }
}
