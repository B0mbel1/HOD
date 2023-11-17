using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public GameObject pickUptext;
    public GameObject clip;
    public AudioClip pickupSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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

                audioSource.PlayOneShot(pickupSound);


                this.gameObject.SetActive(false);
                clip.SetActive(false);
                pickUptext.SetActive(false);
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
