using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    public GameObject cameraHolder;

    void Start()
    {
        MenuPauzy.GameIsPaused = false;
    }

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
