using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwordChangingUser : SkillUser
{
    [SerializeField]
    protected SWORDTYPE _type;

    protected PlayerAttacker _attack;

    protected void SwordChange()
    {
        if (!_attack)
            _attack = transform.parent.GetComponentInParent<PlayerAttacker>();

        _attack.SwordChange((int)_type);
    }
}
