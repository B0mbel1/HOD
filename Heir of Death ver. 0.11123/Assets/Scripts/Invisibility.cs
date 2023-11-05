using System.Collections;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public Renderer custRender;                                 //Individual

    public float TimeLeft;
    public bool PowerOn;
    public float CooldownValue;


    void Start()
    {
        PowerOn = false;
        custRender = GetComponent<Renderer>();//Individual
    }

    void FixedUpdate()
    {
        if (Main.Power == 2)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                PowerActivation();
            }

            if (PowerOn)
            {
                if (TimeLeft > 0)
                {
                    TimeLeft -= Time.fixedDeltaTime;                // daj¹c 5 sekund wyjdzie oko³o 3
                    custRender.enabled = false;                     //Individual
                }
                else
                {
                    PowerDeactivates();

                }
            }
        }
    }

    public void PowerDeactivates()
    {
        PowerOn = false;
        TimeLeft = CooldownValue;
        custRender.enabled = true;
    }

    void PowerActivation()
    {
        PowerOn = true;
    }
}


