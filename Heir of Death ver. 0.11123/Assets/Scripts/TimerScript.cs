using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TimerScript : NetworkBehaviour
{
    public static float TimeLeft = 90f;     //Ustawiæ na 90f;
    public bool TimerOn = false;

    public Text RoundTimer;

    void Start()
    {
        if (Main.gamemode == 0)
        {
            TimerOn = true;
        }
        if(Main.gamemode == 1)
        {
            TimerOn = false;
            RoundTimer.text = "Timer wy³¹czony";
        }
    }

    void Update()
    {
        if (!IsOwner) return;

        if (IsHost)
        {
            if (TimerOn)
            {
                if (TimeLeft > 0)
                {
                    TimeLeft -= Time.deltaTime;
                    UpdateTimer(TimeLeft);
                }
                else
                {
                    TimeLeft = 0;
                    //TimerOn = false;
                }
            }
        }

        if (!IsHost && IsClient)
        {
            if (TimerOn)
            {
                if (TimeLeft > 0)
                {
                    TimeLeft -= Time.deltaTime;
                    UpdateTimer(TimeLeft);
                }
                else
                {
                    TimeLeft = 0;
                    //TimerOn = false;
                }
            }
        }
    }

    public void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        RoundTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
