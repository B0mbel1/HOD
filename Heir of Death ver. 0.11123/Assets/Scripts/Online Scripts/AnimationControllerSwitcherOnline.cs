using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class AnimationControllerSwitcherOnline : NetworkBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController rifleanimations;
    public RuntimeAnimatorController gunanimations;
    [SerializeField] private GameObject gun1;
    [SerializeField] private GameObject gun2;


    void LateUpdate()
    {
        if(!IsOwner) { return; }
        // Sprawdzamy, czy klawisz 1 zosta³ naciœniêty
        if (gun1.activeSelf == true && gun2.activeSelf == false)
        {
            animator.runtimeAnimatorController = rifleanimations;
           
        }

        // Sprawdzamy, czy klawisz 2 zosta³ naciœniêty
        if (gun2.activeSelf == true && gun1.activeSelf == false)
        {
            animator.runtimeAnimatorController = gunanimations;
           
        }
    }
}
