using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class InvisibilityPowerOnline : NetworkBehaviour
{
    private Renderer playerRenderer;
    public Text PowerTimer;

    public float PowerTime;
    public float StartingValue;
    public float CooldownTime;
    public float CooldownStartingValue;
    public float UnlockTime;
    
    public bool isActivated;
    public bool CanPressAbility = true;
    public NetworkVariable<bool> isRendererActive;
    //public NetworkVariable<Renderer> rendereronline;
    public AudioSource audioSource;
    public AudioClip invisibilitySound;
    NetworkObject netObject;

    private void Start()
    {
        if (!IsOwner) return;
        if (Main.Power == 2)
        {
            audioSource.clip = invisibilitySound;

            UnlockTime = StartingValue;
            StartingValue = PowerTime;
            CooldownStartingValue = CooldownTime;

            playerRenderer = GetComponentInChildren<Renderer>();
            
            IsTimerShown(false);
            IsPowerActivated(false);
            isActivated = false;
        }
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        if (IsClient)
        {
            OnRendererChangedClientRpc(isRendererActive.Value);
        }
        if (Main.Power == 2)
        {
            if (CanPressAbility && Input.GetKeyDown(KeyCode.Q))
            {
                IsPowerActivated(true);
                CanPressAbility = false;
                UnlockTime = (Time.time + 10f) + PowerTime;
                audioSource.Play();
            }

            if (!CanPressAbility && Time.fixedTime >= UnlockTime)
            {
                CanPressAbility = true;
            }

            if (isActivated)
            {
                if (PowerTime > 0)
                {
                    PowerTime -= Time.fixedDeltaTime;
                    IsPowerActivated(true);
                }
                else if (PowerTime <= 0)
                {
                    PowerTime = StartingValue;
                    IsTimerShown(true);
                    IsPowerActivated(false);
                }
                CooldownTime = CooldownStartingValue;
            }
            else
            {
                CoodlwonCountdown();
            }
        }
    }

    void CoodlwonCountdown()
    {
        CooldownTime -= Time.fixedDeltaTime;
        UpdateTimer(CooldownTime);

        if (CooldownTime <= 0)
        {
            CooldownTime = CooldownStartingValue;
            IsTimerShown(false);
        }
    }

    void IsPowerActivated(bool activation)
    {
        if (activation == true)
        {
            SendRendererCurrentServerRpc(false);
            playerRenderer.enabled = false;
            isActivated = true;
            
        }
        else
        {
            SendRendererCurrentServerRpc(true);
            isActivated = false;
            playerRenderer.enabled = true;
        }
    }

    bool IsTimerShown(bool timerActive)
    {
        if (timerActive == true)
        {
            return PowerTimer.GetComponent<Text>().enabled = true;
        }
        else
        {
            return PowerTimer.GetComponent<Text>().enabled = false;
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        PowerTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    void InvisibilityOn()
    {
        playerRenderer.enabled = false;

    }
    void InvisibilityOff()
    {
        playerRenderer.enabled = true;
        
    }

    [ServerRpc]
    void SendRendererCurrentServerRpc(bool czy)
    {
        playerRenderer.enabled = czy;
        isRendererActive.Value = czy;
    }

    [ClientRpc]
    void OnRendererChangedClientRpc(bool newvar)
    {
        isRendererActive.Value = newvar;
        playerRenderer.enabled = newvar;
    }
}
