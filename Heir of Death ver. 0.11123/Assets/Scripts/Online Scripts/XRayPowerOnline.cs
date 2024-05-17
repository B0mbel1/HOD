using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class XRayPowerOnline : NetworkBehaviour
{
    public Shader XrayShader;
    public Shader standardShader;
    public Material[] defaultMaterial;
    public Text PowerTimer;

    public float PowerTime;
    public float StartingValue;
    public float CooldownTime;
    public float CooldownStartingValue;
    public float UnlockTime;

    public bool isActivated;
    public bool CanPressAbility = true;

    public AudioSource audioSource;
    public AudioClip xrayPowerSound;

    private void Start()
    {
        if (!IsOwner) return;
        if (Main.Power == 3)
        {
            audioSource.clip = xrayPowerSound;

            UnlockTime = StartingValue;
            StartingValue = PowerTime;
            CooldownStartingValue = CooldownTime;


            SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();
            if (renderer != null)
            {
                standardShader = renderer.material.shader;
            }

            IsTimerShown(false);
            IsPowerActivated(false);
            isActivated = false;
        }
    }

    private void FixedUpdate()
    {
        if (Main.Power == 3)
        {
            if (CanPressAbility && Input.GetKeyDown(KeyCode.Q))
            {
                IsPowerActivated(true);
                CanPressAbility = false;
                UnlockTime = (Time.time + 25f) + PowerTime;

                if (IsHost)
                {
                    audioSource.PlayOneShot(xrayPowerSound);
                    SynchroDzwiekServerRpc(1);
                }

                if (!IsHost && IsClient)
                {
                    audioSource.PlayOneShot(xrayPowerSound);
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

    void IsPowerActivated(bool activation)
    {
        if (IsHost)
        {
            if (activation == true)
            {
                SynchroCheckPlayersServerRpc(0);
                isActivated = true;
            }
            else
            {
                SynchroUncheckPlayersServerRpc(0);
                isActivated = false;
            }
        }

        if (!IsHost && IsClient)
        {
            if (activation == true)
            {

                SynchroCheckPlayersServerRpc(1);
                isActivated = true;
            }
            else
            {
                SynchroUncheckPlayersServerRpc(1);
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
    void CheckPlayersClientRpc(ClientRpcParams clientRpcParams)
    {
        SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();

        if (renderer != null && XrayShader != null)
        {
            renderer.material.shader = XrayShader;
        }
        else
        {
            Debug.LogError("Renderer or myCustomShader is not set.");
        }
    }

    [ClientRpc]
    void UncheckPlayersClientRpc(ClientRpcParams clientRpcParams)
    {
        SkinnedMeshRenderer renderer2 = GetComponent<SkinnedMeshRenderer>();

        if (renderer2 != null && standardShader != null)
        {
            renderer2.material.shader = standardShader;
        }
        else
        {
            Debug.LogError("Renderer or myCustomShader is not set.");
        }
    }

    [ServerRpc(RequireOwnership = false)]
    private void SynchroCheckPlayersServerRpc(ulong a)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { a }
            }
        };

        CheckPlayersClientRpc(clientRpcParams);
    }

    [ServerRpc(RequireOwnership = false)]
    private void SynchroUncheckPlayersServerRpc(ulong b)
    {
        ClientRpcParams clientRpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { b }
            }
        };

        UncheckPlayersClientRpc(clientRpcParams);
    }

    [ClientRpc]
    public void dzwiekXRAYClientRpc(ClientRpcParams clientRpcParams)
    {
        audioSource.PlayOneShot(xrayPowerSound);
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

        dzwiekXRAYClientRpc(clientRpcParams);
    }
}
