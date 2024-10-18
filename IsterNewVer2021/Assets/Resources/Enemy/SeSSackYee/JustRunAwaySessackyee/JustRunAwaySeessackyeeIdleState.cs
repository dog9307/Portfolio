using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustRunAwaySeessackyeeIdleState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int rnd = Random.Range(0, 2);

        animator.SetBool("isJump", (rnd % 2 == 0));
    }
}
