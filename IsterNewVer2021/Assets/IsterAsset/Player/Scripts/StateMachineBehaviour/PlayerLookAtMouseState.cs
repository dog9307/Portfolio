using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtMouseState : StateMachineBehaviour
{
    [SerializeField]
    private bool _isEnterLookAt = false;
    [SerializeField]
    private bool _isUpdateLookAt = false;
    [SerializeField]
    private bool _isExitLookAt = false;

    private PlayerAnimController _anim;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _anim = animator.GetComponent<PlayerAnimController>();

        if (_isEnterLookAt)
        {
            if (_anim)
                _anim.LookMouseDir();
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isUpdateLookAt)
        {
            if (_anim)
                _anim.LookMouseDir();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isExitLookAt)
        {
            if (_anim)
                _anim.LookMouseDir();
        }
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
