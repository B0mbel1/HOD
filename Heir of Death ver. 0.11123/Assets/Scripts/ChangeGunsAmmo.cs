using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGunsAmmo : MonoBehaviour
{
    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;

    [SerializeField] private GameObject ammo1;
    [SerializeField] private GameObject ammo2;


    void Start()
    {
        gun1.GetComponent<GameObject>();
        gun2.GetComponent<GameObject>();
        ammo1.GetComponent<GameObject>();
        ammo2.GetComponent<GameObject>();
    }

    
    void Update()
    {
        if (gun1.activeSelf == true && gun2.activeSelf == false)
        {
            ammo1.active = true;
            ammo2.active = false;
        }
        if (gun2.activeSelf == true && gun1.activeSelf == false)
        {
            ammo2.active = true;
            ammo1.active = false;
        }
    }
}
