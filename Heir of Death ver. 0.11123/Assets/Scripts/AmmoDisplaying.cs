using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplaying : MonoBehaviour
{

    public Text ammo;

    void Start()
    {
        ammo = GetComponent<Text>();
    }

    GunSystem gunsystem;
    void Update()
    {
        ammo.text = GunSystem.bulletsLeft.ToString();
    }
}
