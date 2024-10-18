using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEnd : SkillAnimationEnd
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skillType == null)
            _skillType = typeof(ActiveDash);

        Movable move = animator.GetComponent<Movable>();
        if (move)
            move.isHide = true;

        Undamagable undamagable = animator.GetComponent<Undamagable>();
        if (!undamagable.isUndamagable)
        {
            Damagable damagable = animator.GetComponent<Damagable>();
            damagable.isCanHurt = false;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerSkillUsage skill = animator.GetComponent<PlayerSkillUsage>();
        if (skill)
            skill.SkillEnd(_skillType);

        Movable move = animator.GetComponent<Movable>();
        if (move)
            move.isHide = false;

        Undamagable undamagable = animator.GetComponent<Undamagable>();
        if (!undamagable.isUndamagable)
        {
            Damagable damagable = animator.GetComponent<Damagable>();
            damagable.isCanHurt = true;
        }
    }
}
