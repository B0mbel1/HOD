using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class InvisibilityPowerOnline : NetworkBehaviour
{
    private SkinnedMeshRenderer playerRenderer;
    public Text PowerTimer;

    public float PowerTime;
    public float StartingValue;
    public float CooldownTime;
    public float CooldownStartingValue;
    public float UnlockTime;
    public bool isActivated;
    public bool CanPressAbility = true;

    public SkinnedMeshRenderer rendererLocal;
    public AudioSource audioSource;
    public AudioClip invisibilitySound;

    private void Start()
    {
        if (!IsOwner) return;
        if (Main.Power == 2)
        {
            audioSource.clip = invisibilitySound;

            UnlockTime = StartingValue;
            StartingValue = PowerTime;
            CooldownStartingValue = CooldownTime;

            rendererLocal = GetComponentInChildren<SkinnedMeshRenderer>();

            IsTimerShown(false);
            IsPowerActivated(false);
            isActivated = false;
        }
    }

    private void FixedUpdate()
    {
        if (Main.Power == 2)
        {
            if (CanPressAbility && Input.GetKeyDown(KeyCode.Q) && MenuPauzyOnline.GameIsPaused == false)
            {
                IsPowerActivated(true);
                CanPressAbility = false;
                UnlockTime = (Time.time + 40f) + PowerTime;

                if (IsHost)
                {
                    audioSource.PlayOneShot(invisibilitySound);
                    SynchroDzwiekServerRpc(1);
                }

                if (!IsHost && IsClient)
                {
                    audioSource.PlayOneShot(invisibilitySound);
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

    [ServerRpc]
    public void HostPowerServerRpc(bool show, bool active)
    {
        Power(show, active);
    }

    [ServerRpc]
    public void SynchroPowerServerRpc(bool show, bool active, ulong player)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { player }
            }
        };

        ClientPowerCLientRpc(show, active, clientRpcParams);
    }

    [ClientRpc]
    public void ClientPowerCLientRpc(bool show, bool active, ClientRpcParams clientRpcParams)
    {
        Power(show, active);
    }

    public void Power(bool show, bool active)
    {
        playerRenderer = GetComponentInParent<SkinnedMeshRenderer>();
        playerRenderer.enabled = show;
        isActivated = active;
    }

    public void IsPowerActivated(bool activation)
    {
        if (IsHost)
        {
            if (activation)
            {
                HostPowerServerRpc(false, true);
                SynchroPowerServerRpc(false, true, 1);
                isActivated = true;
            }
            else
            {
                HostPowerServerRpc(true, false);
                SynchroPowerServerRpc(true, false, 1);
                isActivated = false;
            }
        }

        if (IsClient && !IsHost)
        {
            if (activation)
            {
                SynchroPowerServerRpc(false, true, 0);

                isActivated = true;
            }

            if (activation == false)
            {
                SynchroPowerServerRpc(true, false, 0);

                isActivated = false;
            }
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

    [ClientRpc]
    public void dzwiekINVClientRpc(ClientRpcParams clientRpcParams)
    {
        audioSource.PlayOneShot(invisibilitySound);
        audioSource.volume = 0.4f;
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

        dzwiekINVClientRpc(clientRpcParams);
    }
}