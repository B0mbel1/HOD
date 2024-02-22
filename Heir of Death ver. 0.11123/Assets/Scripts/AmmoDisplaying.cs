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

    
    void Update()
    {
        ammo.text = GunSystemOnline.bulletsLeft.ToString();
    }
}
