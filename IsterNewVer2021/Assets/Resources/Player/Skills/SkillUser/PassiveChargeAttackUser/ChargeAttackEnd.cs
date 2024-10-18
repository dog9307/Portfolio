using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAttackEnd : StateMachineBehaviour
{
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerDashDelay delay = animator.GetComponent<PlayerDashDelay>();
        delay.DelayStart();
    }
}
