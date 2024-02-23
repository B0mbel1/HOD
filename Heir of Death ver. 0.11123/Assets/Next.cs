using UnityEngine;

public class Next : MonoBehaviour
{
    public GameObject current;
    public GameObject next;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        if(Input.anyKeyDown)
        {
            current.SetActive(false);
            next.SetActive(true);
        }
    }
}
