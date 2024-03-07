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
        if(!IsOwner) return;
        if (Main.Power == 3)
        {
            audioSource.clip = xrayPowerSound;

            UnlockTime = StartingValue;
            StartingValue = PowerTime;
            CooldownStartingValue = CooldownTime;

            Renderer renderer = GetComponent<Renderer>();
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
        if (!IsOwner) return;
        if (Main.Power == 3)
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
            CheckPlayers();
            isActivated = true;
        }
        else
        {
            UncheckPlayers();
            isActivated = false;
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
    void CheckPlayers()
    {
        foreach(NetworkClient client in NetworkManager.ConnectedClientsList)
        {
            if (client.ClientId != NetworkManager.LocalClientId)
            {
                Renderer clientsRenderer = client.PlayerObject.GetComponentInChildren<Renderer>();
                clientsRenderer.material.shader = XrayShader;
            }
        }
    }

    void UncheckPlayers()
    {
        foreach (NetworkClient client in NetworkManager.ConnectedClientsList)
        {
            if (client.ClientId != NetworkManager.LocalClientId)
            {
                Renderer clientsRenderer = client.PlayerObject.GetComponentInChildren<Renderer>();
                clientsRenderer.material.shader = standardShader;
            }
        }
    }
}
