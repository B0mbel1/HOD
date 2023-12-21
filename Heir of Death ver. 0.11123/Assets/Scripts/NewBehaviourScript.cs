using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MenuPauzy.GameIsPaused = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0,yRotation, 0);
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
