using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplaying_2 : NetworkBehaviour
{
    public Text ammo2;

    [SerializeField] private GameObject gun2;

    void Start()
    {
        ammo2 = GetComponent<Text>();
    }

    void Update()
    {
        if (!IsOwner) { return; }
        ammo2.text = shootingfinal_2.bulletsLeft2.ToString();  
    }
}
