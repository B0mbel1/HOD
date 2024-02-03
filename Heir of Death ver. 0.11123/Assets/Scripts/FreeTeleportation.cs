using UnityEngine;
using UnityEngine.UI;

public class FreeTeleportation : MonoBehaviour
{
    public Camera cam;
    public TeleportationSpot teleportationSpot;
    public static bool teleportState;
    public GameObject gun;
    public GameObject pointer;

    Vector3 destination;

    public float CooldownTime;
    public float CooldownStartingValue;
    public bool cooldownActivated;

    public Text PowerTimer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        teleportState = false;
        CooldownStartingValue = CooldownTime;

        IsTimerShown(false);
    }

    void FixedUpdate()
    {
        if (Main.Power == 1)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                teleportState = !teleportState;
            }

            if (teleportState == true)
            {
                gun.SetActive(false);
                pointer.SetActive(true);
                Line();

                if (Input.GetMouseButtonDown(0))
                {
                    Teleport();
                    IsTimerShown(true);

                    cooldownActivated = true;
                    pointer.SetActive(false);
                    gun.SetActive(true);
                    teleportState = false;
                    CooldownTime = CooldownStartingValue;
                }
            }
            else
            {
                pointer.SetActive(false);
                gun.SetActive(true);
            }

            if (cooldownActivated == true)
            {
                CoodlwonCountdown();
            }
        }
    }

    void Line()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity))
        {
            destination = hit.point;
            teleportationSpot.transform.position = new Vector3(destination.x, destination.y + 0.5f, destination.z);
            teleportationSpot.transform.rotation = Quaternion.LookRotation(hit.normal);


            if (Input.GetMouseButtonDown(0))
            {
                if (teleportationSpot.transform.eulerAngles.x < 280 && teleportationSpot.transform.eulerAngles.x > 260)
                {
                    transform.position = teleportationSpot.transform.position;
                }
                else
                {
                    transform.position = teleportationSpot.transform.position;
                }
            }

        }
    }
    void Teleport()
    {
        transform.position = teleportationSpot.transform.position;
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


    void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        PowerTimer.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
