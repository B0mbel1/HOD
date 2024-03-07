using Unity.Netcode;
using UnityEngine;

public class NewBehaviourScriptOnline : NetworkBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    


    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MenuPauzy.GameIsPaused = false;
        
    }

    void FixedUpdate()
    {
        if (!IsOwner && !IsLocalPlayer) return;
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -70f, 40f);
        

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        //SprawdzCo();
    }

    private void SprawdzCo()
    {
        if (!MenuPauzy.GameIsPaused)
        {
            if (!FreeTeleportation.teleportState)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit co;
                    if (Physics.Raycast(transform.position, transform.forward, out co, Mathf.Infinity))
                    {
                        obrazeniaOK czyOK = co.collider.GetComponent<obrazeniaOK>();
                        if (czyOK != null)
                        {
                            czyOK.zadajObrazenia(10);
                        }
                    }
                }
            }
        }
    }
}
