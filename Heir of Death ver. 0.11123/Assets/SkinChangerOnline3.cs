using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class SkinChangerOnline3 : NetworkBehaviour
{
    [SerializeField] List<Material> materialList = new List<Material>();
    public override void OnNetworkSpawn()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material[] materials = renderer.materials;
        if (ChangeSkin3.skinNumber3 == 0)
        {
            materials[0] = materialList[0];
            materials[1] = materialList[0];
            renderer.materials = materials;
        }
        if (ChangeSkin3.skinNumber3 == 1)
        {
            materials[0] = materialList[1];
            materials[1] = materialList[1];
            renderer.materials = materials;
        }
        if (ChangeSkin3.skinNumber3 == 2)
        {
            materials[0] = materialList[2];
            materials[1] = materialList[2];
            renderer.materials = materials;
        }

    }

    public void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material[] materials = renderer.materials;
        if (ChangeSkin3.skinNumber3 == 0)
        {
            materials[0] = materialList[0];
            materials[1] = materialList[0];
            renderer.materials = materials;
        }
        if (ChangeSkin3.skinNumber3 == 1)
        {
            materials[0] = materialList[1];
            materials[1] = materialList[1];
            renderer.materials = materials;
        }
        if (ChangeSkin3.skinNumber3 == 2)
        {
            materials[0] = materialList[2];
            materials[1] = materialList[2];
            renderer.materials = materials;
        }
    }
}
