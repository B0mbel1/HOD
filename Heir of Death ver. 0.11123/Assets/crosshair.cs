using Unity.Netcode;
using UnityEngine;

public class crosshair : NetworkBehaviour
{
    private RectTransform reticle;
    public Rigidbody playerRigidbody;

    public float restingSize = 75f;
    public float maxSize = 140f;
    public float zoomedSize = 25f;
    public float zoomedSize2 = 60f;
    public float speed;
    private float currentSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!IsOwner) { return; }


        if (MenuPauzyOnline.GameIsPaused == false)
        {
            if (isMoving && !Input.GetMouseButton(1))
            {
                currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime * speed);
            }

            if (isMoving && Input.GetMouseButton(1))
            {
                currentSize = Mathf.Lerp(currentSize, zoomedSize2, Time.deltaTime * speed);
            }

            if (Input.GetMouseButton(1) && !isMoving)
            {
                currentSize = Mathf.Lerp(currentSize, zoomedSize, Time.deltaTime * speed);
            }
            else
            {
                currentSize = Mathf.Lerp(currentSize, restingSize, Time.deltaTime * speed);
            }

            reticle.sizeDelta = new Vector2(currentSize, currentSize);
        }
    }

    bool isMoving
    {
        get
        {
            if (playerRigidbody != null)
            {
                if (playerRigidbody.velocity.sqrMagnitude != 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
