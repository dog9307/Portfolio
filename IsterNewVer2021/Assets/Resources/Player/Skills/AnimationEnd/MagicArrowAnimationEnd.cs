using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowAnimationEnd : SkillAnimationEnd
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skillType == null)
            _skillType = typeof(MagicArrow);

        PlayerMoveController player = animator.GetComponent<PlayerMoveController>();
        if (player)
            player.isDash = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerSkillUsage playerSkill = animator.GetComponent<PlayerSkillUsage>();
        if (!playerSkill.isSkillUsing)
        {
            PlayerMoveController move = animator.GetComponent<PlayerMoveController>();
            if (move)
                move.isDash = false;
        }

        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
