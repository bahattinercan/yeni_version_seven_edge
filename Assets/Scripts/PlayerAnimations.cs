using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator playerAnimator;
    public bool isRunning;
    public bool isCrouch;

    private void Update()
    {
        isRunning = PlayerController.Instance.IsRunning();
        isCrouch = PlayerController.Instance.isCrouching;
        if (!isCrouch)
        {
            if (isRunning)
                playerAnimator.SetBool("run", true);
            else
                playerAnimator.SetBool("run", false);
        }
        else
        {
            if (isRunning)
                playerAnimator.SetBool("crouchWalk", true);
            else
                playerAnimator.SetBool("crouchWalk", false);
        }
    }
}