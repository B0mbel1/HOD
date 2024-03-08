using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] public static int skinNumber;

    public void Change(int value)
    {
        skinNumber = value;
    }
}
