using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class QuietMovementOnline : NetworkBehaviour
{
    public Text PowerTimer;
    public AudioSource audiosource;
    public AudioSource audioSourcePower;
    public AudioClip stealth;

    public float CooldownTime;
    public float CooldownStartingValue;
    public bool cooldownActivated;
    public float PowerTime;
    public float StartingValue;
    public bool isActivated;
    public bool CanPressAbility = true;
    public float UnlockTime;



    void Start()
    {
        if (!IsOwner) return;
        if (Main.Power == 4)
        {
            audioSourcePower.clip = stealth;

            IsTimerShown(false);
            UnlockTime = StartingValue;
            StartingValue = PowerTime;
            CooldownStartingValue = CooldownTime;
            IsPowerActivated(false);
            isActivated = false;
        }
    }

    void FixedUpdate()
    {
        if (!IsOwner) return;
        if (Main.Power == 4)
        {
            if (CanPressAbility && Input.GetKeyDown(KeyCode.Q))
            {
                PlayerMovementOnline.czyWyslacDzwiek = false;
                IsPowerActivated(true);
                CanPressAbility = false;
                UnlockTime = (Time.time + 20f) + PowerTime;
                audioSourcePower.Play();
                if (IsHost)
                {
                    SynchroDzwiekServerRpc(1);

                }

                if(!IsHost && IsClient)
                {
                    SynchroDzwiekServerRpc(0);
                }
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
                    PlayerMovementOnline.czyWyslacDzwiek = true;
                }
                CooldownTime = CooldownStartingValue;
            }
            else
            {
                CoodlwonCountdown();
            }
        }
    }

    void IsPowerActivated(bool activation)
    {
        if (activation == true)
        {
            audiosource.mute = true;
            isActivated = true;
        }
        else
        {
            audiosource.mute = false;
            isActivated = false;
        }
    }

    public bool IsTimerShown(bool timerActive)
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

    void CoodlwonCountdown()
    {
        CooldownTime -= Time.fixedDeltaTime;
        UpdateTimer(CooldownTime);

        if (CooldownTime <= 0)
        {
            CooldownTime = CooldownStartingValue;
            IsTimerShown(false);
            audiosource.mute = false;
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        PowerTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }


    [ClientRpc]
    public void dzwiekQuietClientRpc(ClientRpcParams clientRpcParams)
    {
        audioSourcePower.volume = 0.3f;
        audioSourcePower.PlayOneShot(stealth);
       
    }

    [ServerRpc(RequireOwnership = false)]
    private void SynchroDzwiekServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        dzwiekQuietClientRpc(clientRpcParams);
    }
}
