using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShieldAnimationEnd : SkillAnimationEnd
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skillType == null)
            _skillType = typeof(ActiveShield);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerSkillUsage skill = animator.GetComponent<PlayerSkillUsage>();
        if (skill)
        {
            if (skill.isSkillUsing)
                TriggerUser(animator.GetComponentInChildren<SkillUserManager>());
            else
                skill.SkillEnd(_skillType);
        }
    }
}
