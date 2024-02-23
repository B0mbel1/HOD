using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class TimerScript : NetworkBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    public Text RoundTimer;

    void Start()
    {
        TimerOn = true;
    }
    
    void Update()
    {
        if(!IsOwner) return;
        if(TimerOn)
        {
            if(TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                Debug.Log("Koniec Rundy");
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        RoundTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
