using Unity.Netcode;
using UnityEngine;

public class NewBehaviourScriptOnline : NetworkBehaviour
{
    public float sensX;
    public float sensY;
    float xRotation;
    float yRotation;

    public Transform orientation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        MenuPauzyOnline.GameIsPaused = false;
    }

    void FixedUpdate()
    {
        if (!IsOwner && !IsLocalPlayer) return;

        if(MenuPauzyOnline.GameIsPaused == false)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            yRotation += mouseX;
            xRotation -= mouseY;

            xRotation = Mathf.Clamp(xRotation, -30f, 70f);

            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, yRotation, 0);
        }
        else
        {

        }
        
    }
}
