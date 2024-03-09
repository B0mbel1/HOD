using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Health : NetworkBehaviour
{
    public NetworkVariable<float> health = new NetworkVariable<float>();
    [SerializeField] public float health1;

    private void Update()
    {
        if (!IsOwner) return; // Only allow local player to interact with health

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (IsClient)
            {
                DamageServerRpc(5);
            }
            //else if (IsClient)
            //{
            //   DamageClient(5);
            //}
        }
    }

    public override void OnNetworkSpawn()
    {
        health.Value = 100;
        health1 = health.Value;
        base.OnNetworkSpawn();
    }

    [ServerRpc]
    public void DamageServerRpc(int damage)
    {
        health.Value = health.Value - damage;
        RefreshClientRpc();
    }

    [ClientRpc]
    public void RefreshClientRpc()
    {
        health1 = health.Value;
    }

  




}
