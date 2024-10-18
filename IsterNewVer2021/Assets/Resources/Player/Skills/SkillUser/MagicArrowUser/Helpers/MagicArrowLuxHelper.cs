using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowLuxHelper : MagicArrowUserHelperBase
{
    public float bulletCreatorOffset { get; set; }

    public override GameObject CreateObject()
    {
        Vector2 dir = CommonFuncs.CalcDir(owner.transform.position, owner.bulletCreator.transform.position);

        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.position = owner.transform.position + (Vector3)dir * bulletCreatorOffset;
        
        float angle = CommonFuncs.DirToDegree(dir);
        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        return newBullet;
    }

    public override void Init()
    {
        PlayerAnimController player = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<PlayerAnimController>();
        player.Charging(false);

        if (!effectPrefab)
            effectPrefab = Resources.Load<GameObject>("Player/Bullets/MagicArrow/NormalMagicArrow/Prefab/MagicArrow");
        
        bulletCreatorOffset = 1.0f;
    }

    public override void Release()
    {
    }

    public override void Update()
    {
        owner.transform.localPosition = Vector2.zero;
    }

    public override void UseSkill(GameObject newBullet)
    {
        MagicArrowController controller = newBullet.GetComponent<MagicArrowController>();
        Vector2 dir = CommonFuncs.CalcDir(owner.transform.parent.position, owner.bulletCreator.transform.position);
        if (controller)
        {
            controller.dir = dir;
            //(transform.position - transform.parent.GetComponentInParent<Movable>().center).normalized;
            controller.Init();
        }

        SkillInfo info = GameObject.FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();
        MagicArrow skill = info.FindSkill<MagicArrow>();

        float damageMultiplier = owner.damageMultiplier;
        if (skill.isReversePowerUp)
        {
            int figure = (int)info.FindSkill<PassiveReversePower>().figure;
                 if (figure == 0) damageMultiplier *= 1.2f;
            else if (figure == 1) damageMultiplier *= 1.4f;
            else if (figure == 2) damageMultiplier *= 1.6f;
            else if (figure == 3) damageMultiplier *= 2.0f;
        }

        float additionalDamage = 0.0f;

        if (owner.buff)
        {
            additionalDamage += owner.buff.additionalSkillDamage;
            damageMultiplier *= owner.buff.skillDamageMultiplier;
        }

        Damager damager = newBullet.GetComponent<Damager>();
        if (!damager)
        {
            damager = newBullet.GetComponentInChildren<Damager>();

            if (!damager) return;
        }

        Damage realDamage = damager.damage;
        realDamage.damage = owner.defaultDamage;
                                      // Player Buff   + Skill buff
        realDamage.additionalDamage = additionalDamage + owner.additionalDamage;

        int currentCount = owner.currentCount;
        while (owner.currentCount > 1)
            owner.CoolTimeStart();

        Vector3 scale = newBullet.transform.localScale;
        scale.x *= currentCount;
        scale.y *= currentCount;
        newBullet.transform.localScale = scale;

        realDamage.damageMultiplier = damageMultiplier * currentCount;

        damager.damage = realDamage;
    }
}
