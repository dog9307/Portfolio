using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEndAnimationEnd : StateMachineBehaviour
{
    private ChargingArrowCreator _effect;
    private NormalBulletCreator _creator;

    private PlayerSkillUsage _skill;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _skill = animator.GetComponent<PlayerSkillUsage>();
        _skill.isSkillUsing = true;

        if (!_creator)
            _creator = animator.GetComponentInChildren<NormalBulletCreator>();

        MagicArrowUser user = _skill.GetComponentInChildren<SkillUserManager>().FindUser(typeof(MagicArrow)) as MagicArrowUser;
        _effect = user.GetComponentInChildren<ChargingArrowCreator>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_effect) return;

        _effect.FireBullet();

        _skill.isSkillUsing = false;
    }
}
