using Unity.Netcode;
using UnityEngine;

public class ChangeGun : NetworkBehaviour
{
    [SerializeField] public GameObject gun1;
    [SerializeField] public GameObject gun2;
    
    void Start()
    {
        gun2.SetActive(false);
    }

    void Update()
    {
        if (!IsOwner) return;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gun1.SetActive(true);
            gun2.SetActive(false);
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            gun1.SetActive(false);
            gun2.SetActive(true);
        }
    }
}
