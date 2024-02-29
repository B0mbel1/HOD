using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public Camera playerCamera; // Kamera gracza
    public float zoomFOV = 30f; // Wartoœæ pola widzenia podczas przybli¿ania
    public float defaultFOV = 60f; // Domyœlne pole widzenia
    public float zoomSpeed = 5f; // Szybkoœæ przybli¿ania
   

    private Vector3 originalCameraPosition; // Pocz¹tkowa pozycja kamery

    void Start()
    {
        // Zapisanie pocz¹tkowej pozycji kamery
        originalCameraPosition = playerCamera.transform.localPosition;
    }

    void Update()
    {
        // Sprawdzenie, czy prawy przycisk myszy jest wciœniêty
        if (Input.GetMouseButton(1))
        {
            // Przybli¿enie kamery (zmiana pola widzenia)
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, zoomFOV, Time.deltaTime * zoomSpeed);

            
        }
        else
        {
            // Powrót do domyœlnego pola widzenia
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, defaultFOV, Time.deltaTime * zoomSpeed);

            // Powrót kamery do pocz¹tkowej pozycji
            
        }
    }
}

