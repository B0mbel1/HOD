using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SkinChangerOnline3 : NetworkBehaviour
{
    [SerializeField] List<Material> materialList = new List<Material>();
    public override void OnNetworkSpawn()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (ChangeSkin3.skinNumber3 == 0)
        {
            renderer.material = materialList[0];
        }
        if (ChangeSkin3.skinNumber3 == 1)
        {
            renderer.material = materialList[1];
        }
        if (ChangeSkin3.skinNumber3 == 2)
        {
            renderer.material = materialList[2];
        }
    }

    public void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (ChangeSkin3.skinNumber3 == 0)
        {
            renderer.material = materialList[0];
        }
        if (ChangeSkin3.skinNumber3 == 1)
        {
            renderer.material = materialList[1];
        }
        if (ChangeSkin3.skinNumber3 == 2)
        {
            renderer.material = materialList[2];
        }
    }
}
