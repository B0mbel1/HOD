using UnityEngine;
using UnityEngine.UI;


public class InvisibilityPower : MonoBehaviour
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

    public AudioSource audioSource;
    public AudioClip invisibilitySound;

    private void Start()
    {
        audioSource.clip = invisibilitySound;
        if (Main.Power == 2)
        {
            UnlockTime = StartingValue;     
            StartingValue = PowerTime;
            CooldownStartingValue = CooldownTime;

            playerRenderer = GetComponent<Renderer>();

            IsTimerShown(false);
            IsPowerActivated(false);
            isActivated = false;
        }
    }

    private void FixedUpdate()
    {

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
            playerRenderer.enabled = false;
            isActivated = true;
        }
        else
        {
            playerRenderer.enabled = true;
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
}
