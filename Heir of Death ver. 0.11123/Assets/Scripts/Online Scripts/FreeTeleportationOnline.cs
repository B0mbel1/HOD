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

    private void Update()
    {
        if (Main.Power == 1)
        {
            if (!IsOwner) return;
            if (Input.GetKeyDown(KeyCode.Q) && canTp == true)
            {
                teleportState = !teleportState;
            }
        }
    }

    void FixedUpdate()
    {
        if (Main.Power == 1)
        {
            if (!IsOwner) return;

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
                    StartCoroutine(waitWithTPstate());
                    CooldownTime = CooldownStartingValue;
                    canTp = false;
                    audioSource.Play();

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

    IEnumerator waitWithTPstate()
    {
        yield return new WaitForSeconds(0.1f);
        teleportState = false;
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
