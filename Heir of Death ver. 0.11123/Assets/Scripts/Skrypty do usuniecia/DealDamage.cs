using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    //[SerializeField] private GameObject lewaNoga;
    //[SerializeField] private GameObject prawaNoga;
    //[SerializeField] private GameObject glowa;
    //[SerializeField] private GameObject lewaReka;
    //[SerializeField] private GameObject prawaReka;
    //[SerializeField] private GameObject tors;

    [SerializeField] public GameObject character;

    void Start()
    {
        //lewaNoga.GetComponent<GameObject>();
        //prawaNoga.GetComponent<GameObject>();
        //glowa.GetComponent<GameObject>();
        //tors.GetComponent<GameObject>();
        //lewaReka.GetComponent<GameObject>();
        //prawaReka.GetComponent<GameObject>();

        character.GetComponent<GameObject>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == character)
        {
            if (collision.gameObject.CompareTag("Reka"))
            {
                Debug.Log("urwa³o mi jaja!");
            }
            if (collision.gameObject.CompareTag("Noga"))
            {
                Debug.Log("NUGA!");
            }
            if (collision.gameObject.CompareTag("Tors"))
            {
                Debug.Log("TORY!");
            }
            if (collision.gameObject.CompareTag("Glowa"))
            {
                Debug.Log("GLUWA!");
            }
        }
    }
}
