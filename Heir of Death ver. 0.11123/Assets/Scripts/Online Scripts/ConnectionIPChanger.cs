using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ConnectionIPChanger : NetworkBehaviour
{
    [SerializeField] TextMeshProUGUI ipAddressText;

    void Update()
    {
        ipAddressText.text = "Adres IP hosta: "+ServerManager.AdresIP;
    }
}
