using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTimeSlowEnd : SkillAnimationEnd
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skillType == null)
            _skillType = typeof(ActiveTimeSlow);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerSkillUsage skill = animator.GetComponent<PlayerSkillUsage>();
        if (!skill) return;

        if (!skill.isSkillUsing)
            skill.SkillEnd(_skillType);
        else
            TriggerUser(animator.GetComponentInChildren<SkillUserManager>());
    }
}
