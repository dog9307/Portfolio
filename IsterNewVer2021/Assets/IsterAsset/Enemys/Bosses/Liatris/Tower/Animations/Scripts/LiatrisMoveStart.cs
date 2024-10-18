using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisMoveStart : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (animator.GetComponent<BossMovable>() == null)
        {
            BossMovable _movable = animator.GetComponentInParent<BossMovable>();
            _movable._moveStart = true;
        }
        else
        {
            BossMovable _movable = animator.GetComponent<BossMovable>();
            _movable._moveStart = true;
        }
        //BossMovable _movable = animator.GetComponent<BossMovable>();
        //_movable._moveStart = true;
        //BossMovable _movable = animator.GetComponent<BossMovable>();
        //_movable._moveStart = true;
    }
}