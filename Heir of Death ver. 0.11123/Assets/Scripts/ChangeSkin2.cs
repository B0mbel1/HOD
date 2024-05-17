using UnityEngine;

public class ChangeSkin2 : MonoBehaviour
{
    [SerializeField] public static int skinNumber2;

    public void Change(int value)
    {
        skinNumber2 = value;
    }
}
