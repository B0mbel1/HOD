using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
    private Camera playerCam;

    private void Start()
    {
        playerCam = Camera.main;
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    private void LookAtPlayer()
    {
        transform.LookAt(transform.position + playerCam.transform.rotation * Vector3.forward, playerCam.transform.rotation * Vector3.up);
    }
}
