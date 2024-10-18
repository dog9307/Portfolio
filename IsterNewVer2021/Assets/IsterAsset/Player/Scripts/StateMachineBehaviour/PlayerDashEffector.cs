using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashEffector : StateMachineBehaviour
{
    private PlayerAttacker _attack;

    private ActiveDashUser _user;

    [SerializeField]
    private float _multiplier = 6.0f;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_attack)
            _attack = animator.GetComponent<PlayerAttacker>();

        _attack.speedMultiplier = 0.0f;
        _attack.AfterImageControl(true);

        if (!_user)
            _user = animator.GetComponentInChildren<SkillUserManager>().FindUser(typeof(ActiveDash)) as ActiveDashUser;

        animator.ForceStateNormalizedTime(_user.startNormalizedTime);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        //if (stateInfo.normalizedTime < ActiveDashUser.STANDARD_DELAY)
        //    _attack.speedMultiplier = 0.0f;
        //else if (stateInfo.normalizedTime < 0.9f)
        //    _attack.speedMultiplier = _multiplier;
        //else
        //    _attack.speedMultiplier = 0.0f;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _attack = animator.GetComponent<PlayerAttacker>();
        _attack.AfterImageControl(false);

        _user.SpeedUp();
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
