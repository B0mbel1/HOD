using UnityEngine;

public class Floating : MonoBehaviour
{
    private float amplitude = 0.3f;
    private float amplitudeSpeed = 3f;
    private float startupHeight;

    void Start()
    {
        startupHeight = transform.position.y;
    }

    void Update()
    {
        smoothFloating();
    }

    void smoothFloating()
    {
        float offset = Mathf.Sin(Time.time * amplitudeSpeed) * amplitude;
        float newHeight = startupHeight + offset + 0.75f;

        transform.position = new Vector3(transform.position.x, newHeight, transform.position.z);
    }
}

