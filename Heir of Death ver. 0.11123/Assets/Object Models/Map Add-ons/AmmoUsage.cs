using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public GameObject pickUptext;
    public GameObject clip;

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
                clip.SetActive(false);
                BetterAmmoScript.isBetterAmmoPicked = true;
                BetterAmmoScript.TimerActivation = true;
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
