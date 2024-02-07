using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class Animiations : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d"))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("jumpIdle", true);
        }
        else
        {
            animator.SetBool("jumpIdle", false);
        }

        if(Input.GetKey(KeyCode.LeftControl))
        {
            animator.SetBool("isCrouching", true);
        }
        else
        {
            animator.SetBool("isCrouching", false);
        }

        if (Input.GetKey(KeyCode.Slash))
        {
            animator.SetBool("isEE", true);
        }
        else
        {
            animator.SetBool("isEE", false);
        }
    }
}
