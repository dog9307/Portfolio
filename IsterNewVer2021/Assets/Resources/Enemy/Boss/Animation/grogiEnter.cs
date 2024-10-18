using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grogiEnter : StateMachineBehaviour
{
    [SerializeField]
    private bool _isBossDie = false;

    [SerializeField]
    private bool _isSFXOn = false;
    private SFXPlayer _sfx;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<BossController>().GrogiEnter();

        if (_isBossDie)
            animator.GetComponent<BossController>().BossDie();

        if (_isSFXOn)
        {
            if (!_sfx)
                _sfx = animator.GetComponentInChildren<SFXPlayer>();

            if (_sfx)
                _sfx.PlaySFX("grogi");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("isFirstPhase", false);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{ 
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
