using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;

public class ConnectionIPChanger : NetworkBehaviour
{

    void Start()
    {
        if (IsHost)
        {
            NetworkManager.GetComponent<UnityTransport>().ConnectionData.Address = default;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
