using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerCooldown : MonoBehaviour
{
    public float TimeLeft;
    public bool PowerOn = false;
    public float CooldownValue;

    public Text PowerTimer;

    void Start()
    {
        CooldownValue = TimeLeft;
        PowerOn = false;
        PowerTimer.GetComponent<Text>().enabled = false;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PowerOn = true;
            PowerTimer.GetComponent<Text>().enabled = true;
        }

        if (PowerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                PowerOn = false;
                PowerTimer.GetComponent<Text>().enabled = false;
                ResetCooldown();
            }
        }
    }


    public void ResetCooldown()
    {
        PowerOn = false;
        TimeLeft = CooldownValue;
    }

    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        PowerTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}