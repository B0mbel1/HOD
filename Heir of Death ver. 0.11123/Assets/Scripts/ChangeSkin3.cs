using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin3 : MonoBehaviour
{
    [SerializeField] public static int skinNumber3;

    public void Change(int value)
    {
        skinNumber3 = value;
    }
}
