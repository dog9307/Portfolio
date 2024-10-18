using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownAnimationEnd : SkillAnimationEnd
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skillType == null)
            _skillType = typeof(ActiveCoolTimeDown);
    }
}
