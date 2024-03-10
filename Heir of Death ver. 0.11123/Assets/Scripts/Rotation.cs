using Unity.Netcode;
using UnityEngine;

public class Rotation : NetworkBehaviour
{
    public GameObject cam;

    void FixedUpdate()
    {
        if (!IsOwner) return;
        HandleRotationInput();
    }

    void HandleRotationInput()
    {
        float toRotationY = cam.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, toRotationY, transform.eulerAngles.z);
    }
}
