using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class SkinChangerOnline4 : NetworkBehaviour
{
    [SerializeField] List<Material> materialList = new List<Material>();
    public override void OnNetworkSpawn()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (ChangeSkin4.skinNumber4 == 0)
        {
            renderer.material = materialList[0];
        }
        if (ChangeSkin4.skinNumber4 == 1)
        {
            renderer.material = materialList[1];
        }
        if (ChangeSkin4.skinNumber4 == 2)
        {
            renderer.material = materialList[2];
        }
    }

    public void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (ChangeSkin4.skinNumber4 == 0)
        {
            renderer.material = materialList[0];
        }
        if (ChangeSkin4.skinNumber4 == 1)
        {
            renderer.material = materialList[1];
        }
        if (ChangeSkin4.skinNumber4 == 2)
        {
            renderer.material = materialList[2];
        }
    }
}
