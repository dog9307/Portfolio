using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRangeAttackBuffUser : SkillUser, IObjectCreator
{
    public GameObject effectPrefab { get; set; }

    //class RangeAttackBuffUserHandChanger : SkillUserHandChanger<PassiveRangeAttackBuff>
    //{
    //    public override void BuffOn()
    //    {
    //        _buff.isRangeAttackOn = true;
    //    }

    //    public override void BuffOff()
    //    {
    //        _buff.isRangeAttackOn = false;
    //    }
    //}

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/RangeAttack/Prefab/RangeAttack");

        //DualSwordSetting<RangeAttackBuffUserHandChanger>();
    }

    public GameObject CreateObject()
    {
        Vector3 pos = transform.parent.GetComponentInParent<Movable>().center;

        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.position = pos;

        return newBullet;
    }

    public override void UseSkill()
    {
        // test
        // 각 패시브스킬 UseSkill부분에 버프 적용(필요한 것들만)
        SkillInfo info = transform.parent.GetComponentInParent<SkillInfo>();
        PassiveRangeAttackBuff skill = info.FindSkill<PassiveRangeAttackBuff>();
        if (skill == null) return;

        GameObject newBullet = CreateObject();
        
        float scale = 1.0f;
        if (skill.isReversePowerUp)
        {
            int figure = (int)info.FindSkill<ActiveReversePower>().figure;
            // test
                 if (figure == 0) scale = 1.2f;
            else if (figure == 1) scale = 1.4f;
            else if (figure == 2) scale = 1.6f;
            else if (figure == 3) scale = 2.0f;
        }
        Vector3 localScale = newBullet.transform.localScale;
        localScale.x *= (scale * 2.5f * 1.5f);
        localScale.y *= (scale * 2.5f * 1.5f);
        localScale.z = 1.0f;
        newBullet.transform.localScale = localScale;

        float damageMultiplier = _damageMultiplier;

        float additionalDamage = 0.0f;

        if (_buff)
        {
            additionalDamage += _buff.additionalSkillDamage;
            damageMultiplier *= _buff.skillDamageMultiplier;
        }

        Damager damager = newBullet.GetComponent<Damager>();
        damager.damage = DamageCreator.Create(damager.gameObject, skill.damage, additionalDamage, damageMultiplier, 1.0f);
    }
}
