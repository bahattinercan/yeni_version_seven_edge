using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator playerAnimator;
    public bool isRunning;

    private void Update()
    {
        isRunning = PlayerController.Instance.IsRunning();
        if (isRunning)
            playerAnimator.SetBool("run", true);
        else
            playerAnimator.SetBool("run", false);
    }
}