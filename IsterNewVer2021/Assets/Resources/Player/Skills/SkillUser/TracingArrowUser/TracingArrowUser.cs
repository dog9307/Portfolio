using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowUser : SkillUser, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    public Collider2D target { get; set; }

    public float additionalDamage { get; set; }
    public bool isRandomDebuff { get; set; }

    public bool isStackTracing { get; set; }
    public int maxStack { get; set; }

    public bool isDebuffTimeUp { get; set; }
    
    //public class TracingArrowUserHandChanger : SkillUserHandChanger<TracingArrow>
    //{
    //    public override void BuffOn()
    //    {
    //        _buff.isTracingArrowOn = true;
    //    }

    //    public override void BuffOff()
    //    {
    //        _buff.isTracingArrowOn = false;
    //    }
    //}

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/TracingArrow/Prefab/TracingArrow");

        //DualSwordSetting<TracingArrowUserHandChanger>();

        _buff.isTracingArrowOn = true;

        additionalDamage = 0.0f;
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab) as GameObject;
        newBullet.transform.position = transform.position;

        return newBullet;
    }

    public override void UseSkill()
    {
        if (!target) return;

        GameObject tracingArrow = CreateObject();

        TracingArrowController trace = tracingArrow.GetComponent<TracingArrowController>();
        trace.target = target;

        PlayerSkillUsage playerSkillUsage = FindObjectOfType<PlayerSkillUsage>();
        TracingArrowUser tracingUser = playerSkillUsage.FindUser<TracingArrow>() as TracingArrowUser;

        TracingArrowDamager damager = tracingArrow.GetComponent<TracingArrowDamager>();
        damager.user = tracingUser;

        Damage realDamage = damager.damage;
        realDamage.additionalDamage = additionalDamage;
        damager.damage = realDamage;

        if (isMirage)
        {
            ActiveMirageUser activeMirage = FindObjectOfType<ActiveMirageUser>();
            if (activeMirage)
            {
                if (activeMirage.isAdditionalTracing)
                {
                    GameObject secondArrow = CreateObject();

                    trace = secondArrow.GetComponent<TracingArrowController>();
                    trace.target = target;
                    
                    damager = secondArrow.GetComponent<TracingArrowDamager>();
                    damager.user = tracingUser;

                    realDamage = damager.damage;
                    realDamage.damageMultiplier = 0.0f;
                    damager.damage = realDamage;

                    SpriteRenderer sprite = secondArrow.GetComponent<SpriteRenderer>();
                    sprite.color = Color.gray;
                }
            }
        }

        target = null;
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