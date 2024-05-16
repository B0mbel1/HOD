using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class Set : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ipAddressText;
    [SerializeField] InputField ip;

    [SerializeField] string ipAddress;
    [SerializeField] UnityTransport transport;

    private void Awake()
    {
        transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
    }
}
