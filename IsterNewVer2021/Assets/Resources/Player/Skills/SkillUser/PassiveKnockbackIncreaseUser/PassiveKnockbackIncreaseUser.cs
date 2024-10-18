using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveKnockbackIncreaseUser : SkillUser, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    public GameObject target { get; set; }

    private PassiveKnockbackIncrease _skill;

    public float knockbackFigure { get { return _skill.figure; } }
    public float additionalFigure { get; set; }
    public float totalFigure { get { return knockbackFigure + additionalFigure; } }

    public BOUNCE mode { get; set; }

    public bool isBounceDamage { get; set; }

    public float bounceDamage { get; set; }
    public float additionalDamage { get; set; }

    [SerializeField]
    [Range(0.0f, 1.0f)]private float _slowFigure;
    public float slowFigure { get { return _slowFigure; } set { _slowFigure = value; } }
    [SerializeField]
    private float _slowTime;
    public float slowTime { get { return _slowTime; } set { _slowTime = value; } }
    public float additionalSlowTime { get; set; }
    public float totalSlowTime { get { return (slowTime + additionalSlowTime); } }

    public bool isSecondKnockback { get; set; }
    public bool isSlowBomb { get; set; }

    [SerializeField]
    private GameObject _assistant;
    public GameObject assistant { get { return _assistant; } }

    //public class KnockbackUserHandChanger : SkillUserHandChanger<PassiveKnockbackIncrease>
    //{
    //    public PassiveKnockbackIncreaseUser user { get; set; }

    //    public override void BuffOn()
    //    {
    //        _buff.AddKnockbackIncrease(user.totalFigure);
    //    }

    //    public override void BuffOff()
    //    {
    //        _buff.AddKnockbackIncrease(-user.totalFigure);
    //    }
    //}

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Bullets/KnockbackIncrease/KnockbackIncrease");

        //DualSwordSetting<KnockbackUserHandChanger>();

        //if (_helper != null)
        //    ((KnockbackUserHandChanger)_helper).user = this;

        _skill = FindObjectOfType<PlayerSkillUsage>().FindSkill<PassiveKnockbackIncrease>();

        _buff.AddKnockbackIncrease(totalFigure);

        mode = BOUNCE.NONE;
        damageMultiplier = 1.0f;
    }

    public GameObject CreateObject()
    {
        GameObject effect = GameObject.Instantiate(effectPrefab);
        effect.transform.parent = target.transform;
        effect.transform.localPosition = Vector2.zero;

        return effect;
    }

    public override void UseSkill()
    {
        if (!target) return;
        //if (_helper != null)
        //{
        //    if (!_helper.IsCorrectHand())
        //        return;
        //}

        GameObject newBullet = CreateObject();
        KnockbackDragDownner dragDown = newBullet.GetComponent<KnockbackDragDownner>();
        dragDown.user = this;

        if (isBounceDamage)
        {
            BounceDamager bounce = target.AddComponent<BounceDamager>();
            bounce.user = this;

            Damage damage = DamageCreator.Create(bounce.gameObject, bounceDamage, 0.0f, 1.0f, 0.0f);
            bounce.damage = damage;
            bounce.damageMultiplier = damageMultiplier;

            bounce.mode = mode;
            bounce.figure = slowFigure;
            bounce.totalTime = totalSlowTime;

            bounce.isSecondKnockback = isSecondKnockback;
            bounce.isSlowBomb = isSlowBomb;
        }

        target = null;
    }

    public void KnockbackAssistantTurnOn(bool isTurnOn)
    {
        _assistant.SetActive(isTurnOn);
    }
}
