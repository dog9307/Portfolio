using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingLaserAnimationEnd : StateMachineBehaviour
{
    private ChargingArrowCreator _effect;

    private PlayerSkillUsage _skill;

    private GameObject _laser;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _skill = animator.GetComponent<PlayerSkillUsage>();
        _skill.isSkillUsing = true;

        MagicArrowUser user = _skill.GetComponentInChildren<SkillUserManager>().FindUser(typeof(MagicArrow)) as MagicArrowUser;
        _effect = user.GetComponentInChildren<ChargingArrowCreator>();

        _laser = _effect.FireBullet(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _skill.isSkillUsing = false;

        if (_laser)
            Destroy(_laser);
    }
}
