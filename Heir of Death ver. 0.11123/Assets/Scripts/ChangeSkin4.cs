using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin4 : MonoBehaviour
{
    [SerializeField] public static int skinNumber4;

    public void Change(int value)
    {
        skinNumber4 = value;
    }
}
