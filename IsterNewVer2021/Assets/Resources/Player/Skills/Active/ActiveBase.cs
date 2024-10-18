using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIVE
{
    NONE = -1,
    RANGE,              // ActiveRangeAttack    - PassiveOtherAttack
    MAGICARROW,         // MagicArrow           - TracingArrow
    MIRAGE,             // ActiveMirage         - PassiveMirage
    DASH,               // ActiveDash           - AdditionalAttack
    REVERSEPOWER,       // 삭제
    TIMESLOW,           // ActiveTimeSlow       - PassiveCounterAttack
    MARKER,             // ActiveMarker         - PassiveMarker
    COOLTIMEDOWN,       // ActiveCoolTimeDown   - PassiveCoolTimeDown
    SHIELD,             // ActiveShield         - PassiveRemover
    GRAVITY,            // ActiveGravity        - PassiveKnockbackIncrease
    REROLL,             // ActiveItemReroll     - PassiveFortune
    PARRYING,           // ActiveParrying       - PassiveChargeAttack
    YARAN,              // ActiveYaran
    BUTTERFLY,          // ActiveButterfly
    END
}

public enum SWORDTYPE
{
    NONE = -1,
    BASE,
    HAMMER,
    RAPIER,
    SARCOPHAGUS,
    SCYTHE,
    END
}

public abstract class ActiveBase : SkillBase
{
    protected PlayerAnimController _anim;
    protected ACTIVE _type;

    public override int id { get {  return 100 + (int)_type; } }
    
    public override void Init()
    {
        base.Init();

        _anim = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<PlayerAnimController>();
    }

    public override void UseSkill()
    {
        _anim.UseActiveSkill((int)_type);
        //if (!soundName.Equals(""))
        //    SoundManager.instance.PlaySoundEffect(soundName);
    }
}
