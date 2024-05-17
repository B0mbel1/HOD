using Unity.Netcode;
using UnityEngine;

public class ListenerDetection : NetworkBehaviour
{
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
