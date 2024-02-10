using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class PowerChangeImage : MonoBehaviour
{
    public Sprite tp;
    public Sprite xray;
    public Sprite stealth;
    public Sprite invis;


    Main main;
    void Start()
    {
        Image powerImage = GetComponent<Image>();
        if (Main.Power == 1)
        {
            powerImage.sprite = tp;
        }
        if (Main.Power == 2)
        {
            powerImage.sprite = invis;
        }
        if (Main.Power == 3)
        {
            powerImage.sprite = xray;
        }
        if (Main.Power == 4)
        {
            powerImage.sprite = stealth;
        }
    }


}
