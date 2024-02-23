using Unity.Netcode;
using UnityEngine;

public class TeleportationSpotOnline : NetworkBehaviour
{
    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public MeshRenderer m_Renderer;

    private void Start()
    {
        if(!IsOwner) return;
        target = transform.GetChild(0).GetComponent<Transform>();
        m_Renderer = GetComponent<MeshRenderer>();
    }
}
