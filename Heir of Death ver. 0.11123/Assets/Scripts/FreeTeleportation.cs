using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FreeTeleportation : MonoBehaviour
{
    Camera cam;
    public TeleportationSpot teleportationSpot;
    public static bool teleportState;
    public GameObject gun;
    public GameObject pointer;
    

    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        teleportState = false;
    }

    void Update()
    {
        if (Main.Power == 1)
        {
            Debug.Log(teleportState);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                teleportState = !teleportState;
            }


            if (teleportState == true)
            {
                gun.SetActive(false);
                pointer.SetActive(true);
                Teleport();
            }
            else
            {
                pointer.SetActive(false);
                gun.SetActive(true);
            }
        }
    }

    void Teleport()
    {
        RaycastHit hit;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            teleportationSpot.transform.position = hit.point;
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
}
