using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Unity.Netcode;
using Unity.Networking;

public class RotationOnline : NetworkBehaviour
{

    public NetworkVariable<Quaternion> syncedRotation;
    public GameObject cam;
    public float rotationSpeed = 15f;

    void Update()
    {
        if (IsClient)
        {
            HandleRotationInput();
            OnRotationChangedClientRpc(syncedRotation.Value);
        }
    }

    void HandleRotationInput()
    {

        //transform.rotation = cam.transform.rotation;
        float toRotationY = cam.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, toRotationY, transform.eulerAngles.z);
        if (IsOwner)
        {
            CmdSendRotationToServerServerRpc(transform.rotation);
        }
    }

    [ServerRpc]
    void CmdSendRotationToServerServerRpc(Quaternion rotation)
    {
        syncedRotation.Value = rotation;
    }

    [ClientRpc]
    void OnRotationChangedClientRpc(Quaternion newRotation)
    {
        syncedRotation.Value = newRotation;
        transform.rotation = newRotation;
    }
}
