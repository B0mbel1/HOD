using UnityEngine;
using Unity.Netcode.Transports.UTP;
using Unity.Netcode;
using TMPro;

public class ConnectionIPChanger : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI ipAddressText;

    
    void Update()
    {
        ipAddressText.text = ServerManager.AdresIP;
    }
}
