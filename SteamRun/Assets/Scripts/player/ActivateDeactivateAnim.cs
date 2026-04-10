using UnityEngine;

public class ActivateDeactivateAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void RestartAnim()
    {
        //animator.enabled = false;

        animator.SetBool("isRunning", false);
        animator.SetBool("isGripping", false);
        animator.Play("Idle", -1, 0);
        //animator.Play("Exit", 0);

        //to restart animator and avoid constant animation blend
        animator.enabled = false;
        animator.enabled = true;
    }

    //public void RestartAnim2()
    //{
    //    animator.Update(0f);
    //    animator.CrossFade("Idle", 0f);

    //    animator.SetBool("isRunning", false);
    //    animator.SetBool("isGripping", false);
    //    animator.Play("Idle", -1, 0);

    //    animator.enabled = true;
    //}
}
