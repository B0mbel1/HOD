using Unity.Netcode;
using UnityEngine;

public class RoundCounter : NetworkBehaviour
{
    [SerializeField] public static int counterhost;
    [SerializeField] public static int counterclient;

    private void Update()
    {
        if (!IsOwner) return;
    }

    public override void OnNetworkSpawn()
    {
        counterhost = 0;
        counterclient = 0;
    }
}
