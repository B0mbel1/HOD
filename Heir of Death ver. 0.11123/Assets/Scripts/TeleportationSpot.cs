using UnityEngine;

public class TeleportationSpot : MonoBehaviour
{
    [HideInInspector]
    public Transform target;

    [HideInInspector]
    public MeshRenderer m_Renderer;

    private void Start()
    {
        target = transform.GetChild(0).GetComponent<Transform>();
        m_Renderer = GetComponent<MeshRenderer>();  
    }
}
