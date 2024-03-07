using System.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class FreeTeleportationOnline : NetworkBehaviour
{
    public Camera cam;
    public TeleportationSpotOnline teleportationSpot;
    public static bool teleportState;
    public GameObject gun;
    public GameObject gun2;
    public GameObject pointer;

    Vector3 destination;

    public float CooldownTime;
    public float CooldownStartingValue;
    public bool cooldownActivated;
    public bool canTp;

    public Text PowerTimer;

    public AudioSource audioSource;
    public AudioClip teleportationSound;
    

    private void Start()
    {
        if (!IsOwner) return;
        if (Main.Power == 1)
        {
            audioSource.clip = teleportationSound;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            teleportState = false;
            CooldownStartingValue = CooldownTime;
            canTp = true;

            IsTimerShown(false);
        }
    }
    public int ktoraBron;
    private void Update()
    {
        if (Main.Power == 1)
        {
            if (!IsOwner) return;
            if (Input.GetKeyDown(KeyCode.Q) && canTp == true)
            {
                teleportState = !teleportState;
                if (gun2.activeSelf == true && gun.activeSelf == false)
                {
                    ktoraBron = 2;
                    gun2.SetActive(false);
                }
                if (gun2.activeSelf == false && gun.activeSelf == true)
                {
                    ktoraBron = 1;
                    gun.SetActive(false);
                }
            }
            //if(gun2.activeSelf == true && gun.activeSelf == false)
            //{
            //    gun.SetActive(teleportState);
            //    gun2.SetActive(!teleportState);
            //}
            //if(gun2.activeSelf == false && gun.activeSelf == true)
            //{
            //    gun2.SetActive(teleportState);
            //    gun.SetActive(!teleportState);
            //}
            pointer.SetActive(teleportState);


        }
    }

    void FixedUpdate()
    {
        if (Main.Power == 1)
        {
            if (!IsOwner) return;

            if (teleportState == true)
            {
                Line();

                if (Input.GetKeyUp(KeyCode.Q))
                {
                    Teleport();
                    if (ktoraBron == 1)
                    {
                        gun.SetActive(true);
                    }
                    if (ktoraBron == 2)
                    {
                        gun2.SetActive(true);
                    }
                }
                
                //if (Input.GetKeyDown(KeyCode.Mouse1))
                //{
                //    teleportState = false;
                //}
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
        }
    }

    void Teleport()
    {
        transform.position = teleportationSpot.transform.position;
        IsTimerShown(true);
        cooldownActivated = true;
        

        CooldownTime = CooldownStartingValue;
        canTp = false;
        audioSource.Play();
        teleportState = false;
        
        Debug.Log(teleportationSpot.transform.position);
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
            canTp = true;
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
