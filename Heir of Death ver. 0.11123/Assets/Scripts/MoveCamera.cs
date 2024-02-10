using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class MoveCamera : MonoBehaviour

{
    public Transform cameraPosition;
    public GameObject cameraHolder;
    // Start is called before the first frame update
    void Start()
    {
        MenuPauzy.GameIsPaused = false;
    }

    
    // Update is called once per frame
    void Update()
    {
        
        transform.position = cameraPosition.position;
    }
}
