using UnityEngine;

public class AnimationControllerSwitcher : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController rifleanimations;
    public RuntimeAnimatorController gunanimations;
    //public MonoBehaviour script1; 
    //public MonoBehaviour script2;

    void Update()
    {
        // Sprawdzamy, czy klawisz 1 zosta³ naciœniêty
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.runtimeAnimatorController = rifleanimations;
           // if (script1 != null)
           ////     script1.enabled = true;
            //if (script2 != null)
            //    script2.enabled = false;
        }

        // Sprawdzamy, czy klawisz 2 zosta³ naciœniêty
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.runtimeAnimatorController = gunanimations;
           // if (script1 != null)
           //     script1.enabled = false;
           // if (script2 != null)
           //     script2.enabled = true;
        }
    }
}
