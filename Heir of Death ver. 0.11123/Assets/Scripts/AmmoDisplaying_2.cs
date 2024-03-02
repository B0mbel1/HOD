using System;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplaying_2 : MonoBehaviour
{
    public Text ammo2;

    [SerializeField] private GameObject gun2;
    //public static int gun1Ammo;

    void Start()
    {
        ammo2 = GetComponent<Text>();
    }

    void Update()
    {
        ammo2.text = shootingfinal_2.bulletsLeft2.ToString();  
    }
}
