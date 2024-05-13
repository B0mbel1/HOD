using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class BetterAmmoScript : NetworkBehaviour
{
    public float AmmoTimeLeft;
    public float AmmoTimeReset;
    public Text AmmoTimer;

    public static bool isBetterAmmoPicked;
    public static bool TimerActivation;
    public static bool isBetterAmmoActivated = false;

    void Start()
    {
        AmmoTimer.GetComponent<Text>().enabled = false;
    }

    void Update()
    {
        if (TimerActivation && isBetterAmmoPicked)
        {
            isBetterAmmoActivated = true;
            AmmoTimer.GetComponent<Text>().enabled = true;
            if (AmmoTimeLeft > 0)
            {
                AmmoTimeLeft -= Time.deltaTime;
                UpdateTimer(AmmoTimeLeft);
            }
            else
            {
                TimerActivation = false;
                isBetterAmmoPicked = false;
                AmmoTimer.GetComponent<Text>().enabled = false;
                AmmoTimeLeft = AmmoTimeReset;
                isBetterAmmoActivated = false;
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        AmmoTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
