using UnityEngine;

public class FreeTeleportation : MonoBehaviour
{
    public Camera cam;
    public TeleportationSpot teleportationSpot;
    public static bool teleportState;
    public GameObject gun;
    public GameObject pointer;

    Vector3 destination;
    


    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        teleportState = false;
    }

    void FixedUpdate()
    {
        if (Main.Power == 1)
        {
            //Debug.Log(teleportState);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                teleportState = !teleportState;
            }

            if (teleportState == true)
            {
                gun.SetActive(false);
                pointer.SetActive(true);
                Line();
                if(Input.GetMouseButtonDown(0))
                {
                    Teleport();
                }
            }
            else
            {
                pointer.SetActive(false);
                gun.SetActive(true);
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
}
