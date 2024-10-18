using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisPhaseChangeStart : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossDamagableCondition condition = animator.GetComponentInParent<BossDamagableCondition>();
        if (condition)
            condition.isPhaseChanging = true;
    }
}