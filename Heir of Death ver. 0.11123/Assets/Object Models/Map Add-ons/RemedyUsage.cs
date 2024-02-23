using UnityEngine;

public class RemedyUsage : MonoBehaviour
{ 
    public GameObject remedy;
    public GameObject pickUptext;

    public AudioClip pickupSound;
    public AudioSource pickupSource;

    private void Start()
    {
        pickUptext.SetActive(false);
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUptext.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickUptext.SetActive(false);
                this.gameObject.SetActive(false);
                remedy.SetActive(false);

                PlayerHealth.RestoreHealth(10);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUptext.SetActive(false);
        }
    }
}

    




