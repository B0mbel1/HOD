using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] public static int skinNumber;

    public void Change(int value)
    {
        skinNumber = value;
    }
}
