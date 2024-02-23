using UnityEngine;
using UnityEngine.UI;

public class QuietMovement : MonoBehaviour
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
        audioSourcePower.clip = stealth;
        if(Main.Power == 4)
        {
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
        if (Main.Power == 4)
        {
            if (CanPressAbility && Input.GetKeyDown(KeyCode.Q))
            {
                IsPowerActivated(true);
                CanPressAbility = false;
                UnlockTime = (Time.time + 10f) + PowerTime;
                audioSourcePower.Play();
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
}


