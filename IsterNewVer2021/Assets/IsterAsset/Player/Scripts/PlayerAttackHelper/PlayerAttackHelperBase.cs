using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAttackHelperBase
{
    public PlayerAttacker attacker { get; set; }

    public PlayerSkillUsage skill { get { return attacker.skill; } }
    public PlayerDashDelay delay { get { return attacker.delay; } }

    public abstract void Init();
    public abstract void Release();

    public abstract bool IsTriggered();
    public abstract void AttackStart();
    public abstract void AttackEnd();

    protected virtual bool IsCanAttack()
    {
        if (!attacker) return false;
        return attacker.IsCanAttack();
    }
}
