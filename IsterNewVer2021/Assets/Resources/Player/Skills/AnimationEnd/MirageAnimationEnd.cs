using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageAnimationEnd : SkillAnimationEnd
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_skillType == null)
            _skillType = typeof(ActiveMirage);
    }
}
