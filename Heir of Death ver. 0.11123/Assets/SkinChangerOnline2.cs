using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SkinChangerOnline2 : NetworkBehaviour
{
    [SerializeField] List<Material> materialList = new List<Material>();

    public override void OnNetworkSpawn()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (ChangeSkin2.skinNumber2 == 0)
        {
            renderer.material = materialList[0];
        }

        if (ChangeSkin2.skinNumber2 == 1)
        {
            renderer.material = materialList[1];
        }

        if (ChangeSkin2.skinNumber2 == 2)
        {
            renderer.material = materialList[2];
        }
    }

    public void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();

        if (ChangeSkin2.skinNumber2 == 0)
        {
            renderer.material = materialList[0];
        }

        if (ChangeSkin2.skinNumber2 == 1)
        {
            renderer.material = materialList[1];
        }

        if (ChangeSkin2.skinNumber2 == 2)
        {
            renderer.material = materialList[2];
        }
    }
}
