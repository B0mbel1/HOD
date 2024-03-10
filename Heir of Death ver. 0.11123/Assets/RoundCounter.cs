using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoundCounter : NetworkBehaviour
{
    
    [SerializeField] public static int counterhost;
    [SerializeField] public static int counterclient;

    private void Update()
    {
        if(!IsOwner) return;
        Debug.Log("Host" + counterhost);
        Debug.Log("Klient" + counterclient);
    }

    public override void OnNetworkSpawn()
    {
        counterhost = 0;
        counterclient = 0;
    }

    

    
}
