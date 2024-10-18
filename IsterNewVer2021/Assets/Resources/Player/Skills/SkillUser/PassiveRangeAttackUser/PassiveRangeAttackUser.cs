using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRangeAttackUser : SkillUser, IObjectCreator
{
    public GameObject effectPrefab { get; set; }

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/RangeAttack/Prefab/RangeAttack");
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
        GameObject newBullet = CreateObject();

        // test
        // 각 패시브스킬 UseSkill부분에 버프 적용(필요한 것들만)
        SkillInfo info = transform.parent.GetComponentInParent<SkillInfo>();
        PassiveRangeAttack skill = info.FindSkill<PassiveRangeAttack>();
        float scale = 1.0f;
        if (skill.isReversePowerUp)
        {
            int figure = (int)info.FindSkill<ActiveReversePower>().figure;
            // test
                 if (figure == 0) scale = 3.0f;
            else if (figure == 1) scale = 1.4f;
            else if (figure == 2) scale = 1.6f;
            else if (figure == 3) scale = 2.0f;
        }
        Vector3 localScale = newBullet.transform.localScale;
        localScale.x *= scale;
        localScale.y *= scale;
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
