using UnityEngine;
using UnityEngine.UI;


public class Rotation : MonoBehaviour
{
    
   
    public GameObject cam;
    

    void Update()
    {
        
        
            HandleRotationInput();
            
        
    }

    void HandleRotationInput()
    {

        //transform.rotation = cam.transform.rotation;
        float toRotationY = cam.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, toRotationY, transform.eulerAngles.z);
        
    }

    
}
