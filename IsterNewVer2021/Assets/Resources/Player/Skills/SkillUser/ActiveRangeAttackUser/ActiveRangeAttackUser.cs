using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeAttackUser : FloorSkillUser
{
    public float scaleFactor { get { return (skill.scale * scaleMultiplier); } }

    public float scaleMultiplier { get; set; }

    public bool isDebuffDamaging { get; set; }
    public float additionalDamage { get; set; }

    public float delayTime { get; set; }

    public bool isDebuffTimeUp { get; set; }
    public bool isRandomDebuff { get; set; }

    protected override void Start()
    {
        base.Start();

        SkillInfo info = FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();
        skill = info.FindSkill<ActiveRangeAttack>();

        scaleMultiplier = 1.0f;
        delayTime = 0.0f;
    }

    public void RandomDebuff(DebuffInfo debuffInfo)
    {
        if (!isRandomDebuff) return;
        if (!debuffInfo) return;

        DEBUFF_TYPE type = (DEBUFF_TYPE)Random.Range((int)DEBUFF_TYPE.SLOW, (int)DEBUFF_TYPE.END);

        DebuffBase debuff = null;
        switch (type)
        {
            case DEBUFF_TYPE.SLOW:
                debuff = new DebuffSlow();
                debuff.totalTime = 3.0f;
                break;

            case DEBUFF_TYPE.POISON:
                debuff = new DebuffPoison();
                debuff.totalTime = 5.0f;
                break;

            case DEBUFF_TYPE.ELECTRIC:
                debuff = new DebuffElectric();
                debuff.totalTime = 5.0f;
                break;

            case DEBUFF_TYPE.WEAKNESS:
                debuff = new DebuffWeakness();
                debuff.totalTime = 3.0f;
                break;

            case DEBUFF_TYPE.RAGE:
                debuff = new DebuffRage();
                debuff.totalTime = 5.0f;
                break;
        }

        if (debuff == null) return;

        debuffInfo.AddDebuff(debuff);
    }
}
