using UnityEngine;

public class RemedyUsage : MonoBehaviour
{ 
    public GameObject remedy;
    public GameObject pickUptext;

    private void Start()
    {
        pickUptext.SetActive(false);
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

    




