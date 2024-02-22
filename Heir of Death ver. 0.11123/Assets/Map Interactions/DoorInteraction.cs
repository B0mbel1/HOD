using System.Collections;
using System.Collections.Generic;
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
    private bool defaultState;

    void Start()
    {
        interactionText.SetActive(false);
        doorSource = GetComponent<AudioSource>();
        doorAnimator = door.GetComponent<Animator>();
        isOpened = false;
        defaultState = false;
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
                interactionText.SetActive(false);
                if (isOpened)
                {
                    CloseDoor();
                    
                }
                if(!isOpened)
                {
                    OpenDoor();
                }
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

    void OpenDoor()
    {
            
            doorAnimator.SetBool("canOpen", true);
            if (doorSound != null && doorSource != null)
            {
                doorSource.PlayOneShot(doorSound);
                doorAnimator.Play("OpenDoor");
                isOpened = true;
                defaultState = false;
            }
        
        

    }

    void CloseDoor()
    {
        doorAnimator.SetBool("canClose", true);
        if (doorSound != null && doorSource != null)
        {
            doorSource.PlayOneShot(doorSound);
            doorAnimator.Play("CloseDoor");
            isOpened = false;
            defaultState = true;
            Invoke("WaitDoor", 1.5f);
        }
    }

    public void WaitDoor()
    {
        doorAnimator.Rebind();
    }

    
}

