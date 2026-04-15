using UnityEngine;

public class ActivateDeactivateAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void RestartAnim()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isGripping", false);
        animator.Play("Idle", -1, 0);
    }
}
