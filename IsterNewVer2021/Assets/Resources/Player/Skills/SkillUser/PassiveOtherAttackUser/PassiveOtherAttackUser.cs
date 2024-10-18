using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveOtherAttackUser : SkillUser, IObjectCreator
{
    private PassiveOtherAttackCreator _creator;

    public int level { get; set; }
    public float scaleFactor { get; set; }
    
    public bool isStingForm { get; set; }

    private NormalBulletCreator _normalCreator;
    public GameObject effectPrefab { get; set; }

    public bool isKnockback { get; set; }
    public bool isPoisonFlooring { get; set; }
    public bool isDebuffDamaging { get; set; }

    protected override void Start()
    {
        if (isMirage) return;

        base.Start();

        Init();
    }

    public override void Init()
    {
        base.Start();

        _creator = GetComponentInChildren<PassiveOtherAttackCreator>();
        _creator.effectPrefab = Resources.Load<GameObject>("Player/Bullets/OtherAttack/Prefab/OtherAttack");

        _normalCreator = _info.GetComponentInChildren<NormalBulletCreator>();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/OtherAttack/Prefab/OtherSting");

        scaleFactor = 1.0f;
    }

    public override void UseSkill()
    {
        GameObject newBullet = _creator.CreateObject();

        PassiveOtherAttackUser otherAttackUser = FindObjectOfType<PlayerSkillUsage>().FindUser<PassiveOtherAttack>() as PassiveOtherAttackUser;

        Vector3 scale = newBullet.transform.localScale;
        float realScale = otherAttackUser.scaleFactor;

        if (isMirage)
        {
            ActiveMirageUser activeMirage = FindObjectOfType<ActiveMirageUser>();
            if (activeMirage)
            {
                if (activeMirage.isOtherScaleUp)
                    realScale *= 1.5f;
            }
        }

        scale.x *= realScale;
        scale.y *= realScale;

        newBullet.transform.localScale = scale;

        OtherAttackDamager damager = newBullet.GetComponentInChildren<OtherAttackDamager>();
        if (damager)
        {
            damager.user = otherAttackUser;
            if (isKnockback)
            {
                Damage realDamage = damager.damage;
                realDamage.knockbackFigure = level * 5.0f + 5.0f;
                damager.damage = realDamage;
            }
        }

        OtherFlooringDebuffCreator debuffCreator = newBullet.GetComponentInChildren<OtherFlooringDebuffCreator>();
        if (debuffCreator)
        {
            debuffCreator.user = otherAttackUser;
            debuffCreator.transform.position = transform.position;
        }

        if (isStingForm)
        {
            GameObject sting = CreateObject();
            sting.transform.localScale = scale;

            OtherAttackDamager stingDamager = sting.GetComponentInChildren<OtherAttackDamager>();
            stingDamager.user = otherAttackUser;
            stingDamager.isDebuffDamaging = isDebuffDamaging;
        }
    }

    public void ChangePrefab(GameObject newPrefab)
    {
        _creator.effectPrefab = newPrefab;
    }

    public GameObject CreateObject()
    {
        GameObject sting = Instantiate(effectPrefab);
        sting.transform.position = _normalCreator.transform.position;
        
        Vector2 dir = _normalCreator.dir;
        float angle = CommonFuncs.DirToDegree(dir);
        sting.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        Vector3 scale = sting.transform.localScale;
        scale.x *= scaleFactor;
        scale.y *= scaleFactor;
        sting.transform.localScale = scale;

        return sting;
    }
}
