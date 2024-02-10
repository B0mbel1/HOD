using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public class MoveCameraOnline : NetworkBehaviour

{
    public Transform cameraPosition;
    public GameObject cameraHolder;
    // Start is called before the first frame update
    void Start()
    {
        MenuPauzy.GameIsPaused = false;
    }

    public override void OnNetworkSpawn()
    { // This is basically a Start method
        cameraHolder.SetActive(IsOwner);
        base.OnNetworkSpawn(); // Not sure if this is needed though, but good to have it.
    }
    // Update is called once per frame
    void Update()
    {
        if (!IsOwner && !IsLocalPlayer) return;
        transform.position = cameraPosition.position;
    }
}
