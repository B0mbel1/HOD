using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class DoorInteraction : NetworkBehaviour
{
    public GameObject interactionText;
    public GameObject door;

    public AudioClip doorSound;
    public AudioSource doorSource;

    private Animator doorAnimator;
    private bool isOpened;

    void Start()
    {
        interactionText.SetActive(false);
        doorSource = GetComponent<AudioSource>();
        doorAnimator = door.GetComponent<Animator>();
        isOpened = false;
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
                interactionText.SetActive(false);
                StartCoroutine(OpenDoor());
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

    IEnumerator OpenDoor()
    {
        if (isOpened == false)
        {
            doorAnimator.SetBool("canOpen", true);
            if (doorSound != null && doorSource != null)
            {
                doorSource.PlayOneShot(doorSound);
                doorAnimator.Play("OpenDoor", 1);
                isOpened = true;
            }
        }
        else if (isOpened == true)
        {
            doorAnimator.SetBool("canClose", true);
            if (doorSound != null && doorSource != null)
            {
                doorSource.PlayOneShot(doorSound);
                doorAnimator.Play("CloseDoor", 1);
                isOpened = false;
                yield return new WaitForSeconds(1.5f);
                doorAnimator.Rebind();
            }
        }
    }

    IEnumerator doorRebind()
    {
        yield return new WaitForSeconds(1.5f);
    }
}

