using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplaying : NetworkBehaviour
{
    public Text ammo;

    [SerializeField] private GameObject gun1;
    //public static int gun1Ammo;

    void Start()
    {
        ammo = GetComponent<Text>();
    }

    void Update()
    {
        if (!IsOwner) { return; }
        ammo.text = shootingfinal.bulletsLeft1.ToString();  
    }
}
