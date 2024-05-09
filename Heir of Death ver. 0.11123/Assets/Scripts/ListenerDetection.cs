using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ListenerDetection : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!IsOwner)
        {
            AudioListener audioListener = GetComponentInChildren<AudioListener>();
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
        }
    }

}
