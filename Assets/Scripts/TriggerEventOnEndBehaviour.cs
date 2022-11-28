using UnityEngine;
using UnityEngine.UI;

public class TriggerEventOnEndBehaviour : StateMachineBehaviour
{
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveAccurateChecker.Instance.SetRandomMove();
        MoveAccurateChecker.canMove = true;
        foreach (Button btn in MoveAccurateChecker.Instance.buttons)
            btn.interactable = true;
    }
}