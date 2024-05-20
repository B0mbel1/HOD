using Unity.Netcode;
using UnityEngine;

public class MoveCameraOnline : NetworkBehaviour
{
    public Transform cameraPosition;
    public GameObject cameraHolder;
    public GameObject Hud;

    void Start()
    {
        MenuPauzyOnline.GameIsPaused = false;
    }

    public override void OnNetworkSpawn()
    { 
        cameraHolder.SetActive(IsOwner);
        Hud.SetActive(IsOwner);
        base.OnNetworkSpawn(); 
    }

    void FixedUpdate()
    {
        if (!IsOwner && !IsLocalPlayer) return;
        transform.position = cameraPosition.position;
    }
}
