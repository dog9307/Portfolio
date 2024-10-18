using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowAnimController : StateMachineBehaviour
{
    private PlayerAttacker _attack;
    private LookAtMouse _look;
    private PlayerSkillUsage _skill;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_attack)
            _attack = animator.GetComponent<PlayerAttacker>();

        if (!_look)
            _look = animator.GetComponent<LookAtMouse>();

        if (!_skill)
            _skill = animator.GetComponent<PlayerSkillUsage>();

        _attack.speedMultiplier = 0.0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
        if (!_attack.isAttacking)
        {
            _attack.dir = _look.dir;
            if (stateInfo.normalizedTime < 0.3f)
                _attack.speedMultiplier = 2.0f;
            else
                _attack.speedMultiplier = 0.0f;
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        PlayerMoveController player = animator.GetComponent<PlayerMoveController>();
        if (player)
            player.isDash = false;
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
