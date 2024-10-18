using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemRerollAnimationEnd : SkillAnimationEnd
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skillType == null)
            _skillType = typeof(ActiveItemReroll);

        TriggerUser(animator.GetComponentInChildren<SkillUserManager>());
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerSkillUsage skill = animator.GetComponent<PlayerSkillUsage>();
        if (skill)
        {
            //if (skill.isSkillUsing)
            //    TriggerUser(animator.GetComponentInChildren<SkillUserManager>());

            skill.SkillEnd(_skillType);
        }
    }
}
