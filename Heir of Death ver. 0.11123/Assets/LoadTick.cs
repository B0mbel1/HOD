using UnityEngine;
using UnityEngine.UI;

public class LoadTick : MonoBehaviour
{
    public Toggle t1;
    public Toggle t2;
    public Toggle t3;
    public Toggle t4;
    public Toggle t5;
    
    void Update()
    {
        if(Main.Character == 1)
        {
            t1.isOn = true;
            t2.isOn = false;
            t3.isOn = false;
            t4.isOn = false;
            t5.isOn = false;
        }
        else if(Main.Character == 2) 
        {
            t2.isOn = true;
            t1.isOn = false;
            t3.isOn = false;
            t4.isOn = false;
            t5.isOn = false;
        }
        else if (Main.Character == 3)
        {
            t3.isOn = true;
            t2.isOn = false;
            t1.isOn = false;
            t4.isOn = false;
            t5.isOn = false;
        }
        else if (Main.Character == 4)
        {
            t4.isOn = true;
            t2.isOn = false;
            t3.isOn = false;
            t1.isOn = false;
            t5.isOn = false;
        }
        else if (Main.Character == 5)
        {
            t4.isOn = false;
            t2.isOn = false;
            t3.isOn = false;
            t1.isOn = false;
            t5.isOn = true;
        }
    }
}
