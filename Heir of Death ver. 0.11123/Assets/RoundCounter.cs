using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoundCounter : NetworkBehaviour
{
    public static RoundCounter Instance { get; private set; }
    [SerializeField] public static int counter0;
    [SerializeField] public static int counter1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
    }

    private void Start()
    {
        counter0 = 0;
        counter1 = 0;
    }

    public void UpdateClientLocally()
    {
        counter1 += 1;
    }

    public void UpdateHostLocally()
    {
        counter0 += 1;
    }

    [ServerRpc(RequireOwnership = false)]
    public void UpdateClientCounterServerRpc()
    {
        counter1 += 1;
    }


    [ClientRpc]
    public void GetClientCounterClientRpc()
    {
        
            counter0 += 1;
        
    }

    
}
