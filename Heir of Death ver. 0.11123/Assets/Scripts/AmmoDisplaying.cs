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

    //GunSystem gunsystem;
    shootingfinal shooting_final;
    void Update()
    {
       ammo.text = GunSystemOnline.bulletsLeft.ToString();
        //ammo.text = shootingfinal.bulletsLeft.ToString();
    }
}
