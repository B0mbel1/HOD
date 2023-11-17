//using UnityEngine;

//public class LewitacjaIObracanie : MonoBehaviour
//{
//    public float wysokoscNadZiemia = 0.1f;
//    private float predkoscObrotu = 5f;

//    void Update()
//    {
//        LewitujIObracaj();
//    }

//    void LewitujIObracaj()
//    {
//        transform.position = new Vector3(transform.position.x, wysokoscNadZiemia, transform.position.z);

//        transform.Rotate(Vector3.up * predkoscObrotu * Time.deltaTime);
//    }
//}


using UnityEngine;

public class LewitacjaPlynna : MonoBehaviour
{
    private float amplitudeLewitacji = 0.3f;
    private float predkoscLewitacji = 3f;

    private float poczatkowaWysokosc;

    void Start()
    {
        poczatkowaWysokosc = transform.position.y;
    }

    void Update()
    {
        LewitujPlynnie();
    }

    void LewitujPlynnie()
    {
        // Obliczamy now¹ wysokoœæ z wykorzystaniem funkcji sinus
        float offset = Mathf.Sin(Time.time * predkoscLewitacji) * amplitudeLewitacji;
        float nowaWysokosc = poczatkowaWysokosc + offset + 0.75f;

        // Ustawiamy pozycjê obiektu
        transform.position = new Vector3(transform.position.x, nowaWysokosc, transform.position.z);
    }
}

