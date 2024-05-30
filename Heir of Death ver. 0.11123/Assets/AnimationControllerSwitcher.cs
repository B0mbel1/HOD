using UnityEngine;

public class AnimationControllerSwitcher : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController rifleanimations;
    public RuntimeAnimatorController gunanimations;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.runtimeAnimatorController = rifleanimations;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.runtimeAnimatorController = gunanimations;
        }
    }
}
