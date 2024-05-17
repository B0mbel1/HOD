using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera playerCamera;
    public float zoomFOV = 30f;
    public float defaultFOV = 60f;
    public float zoomSpeed = 5f;

    private Vector3 originalCameraPosition;

    void Start()
    {
        originalCameraPosition = playerCamera.transform.localPosition;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, defaultFOV, Time.deltaTime * zoomSpeed);
        }
    }
}

