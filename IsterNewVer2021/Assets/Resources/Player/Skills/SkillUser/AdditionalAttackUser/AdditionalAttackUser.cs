using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalAttackUser : SwordChangingUser
{
    private AdditionalAttack _skill;
    private HAND _relativeHand;
    public HAND relativeHand { get { return _relativeHand; } }

    [SerializeField]
    private float _totalTime = 1.0f;
    private float _currentTime;

    public bool isCoolTime { get { return (_currentTime < _totalTime); } }
    
    public int skillCount { get { if (_skill != null) return _skill.maxCount; else return 0; } }
    public int additionalCount { get; set; }
    public int maxCount { get { return (skillCount + additionalCount); } }
    public int currentCount { get; set; }

    public bool isSkip { get; set; }
    public bool isCorrectHand { get; set; }

    // skill tree
    public bool isSlow { get; set; }
    public bool isPoison { get; set; }
    public bool isElectric { get; set; }
    public bool isWeakness { get; set; }
    public bool isRage { get; set; }

    public bool isAbnormalAttack { get { return (isSlow || isPoison || isElectric || isWeakness || isRage); } }

    public bool isSpeedUp { get; set; }

    //public class AdditionalAttackUserHandChanger : SkillUserHandChanger<AdditionalAttack>
    //{
    //    public AdditionalAttackUser owner { get; set; }

    //    public override void BuffOn()
    //    {
    //        if (owner)
    //            owner.isCorrectHand = true;
    //    }

    //    public override void BuffOff()
    //    {
    //        if (owner)
    //            owner.isCorrectHand = false;
    //    }
    //}

    protected override void Start()
    {
        base.Start();

        isSkip = false;

        PlayerSkillUsage skill = transform.parent.GetComponentInParent<PlayerSkillUsage>();
        if (!skill) return;

        _skill = skill.FindSkill<AdditionalAttack>();
        _relativeHand = skill.GetSkillHand(_skill.GetType());

        _currentTime = _totalTime;
        currentCount = maxCount;
        isCorrectHand = true;

        SwordChange();
        _attack.ChangeHelper(new PlayerDashAttackHelper());

        //_helper = new AdditionalAttackUserHandChanger();
        //((AdditionalAttackUserHandChanger)_helper).owner = this;
        //DualSwordSetting<AdditionalAttackUserHandChanger>();
    }

    void Update()
    {
        //if (_helper != null)
        //    _helper.Update();

        if (!isCoolTime) return;

        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _totalTime)
        {
            currentCount = maxCount;
            NormalAttackDamager.isXFlip = false;
        }
    }

    public override void UseSkill()
    {
        if (!_attack) return;

        if (currentCount > 0)
        {
            _currentTime = 0.0f;
            currentCount--;
            isSkip = true;

            if (isSpeedUp)
                SpeedUp();

            NormalAttackDamager.isXFlip = !NormalAttackDamager.isXFlip;
        }
    }

    public void SpeedUp()
    {
        PlayerMoveController player = GameObject.FindObjectOfType<PlayerMoveController>();
        PlayerEffectManager em = player.GetComponentInChildren<PlayerEffectManager>();
        GameObject effect = em.FindEffect("PassiveSpeedUp");
        if (effect)
        {
            SpeedUpEffect speedUp = effect.GetComponent<SpeedUpEffect>();
            if (!speedUp) return;

            speedUp.totalTime = 1.0f;
            speedUp.figure = 5.0f;

            if (effect.activeSelf)
                speedUp.BuffOn();
            else
                effect.SetActive(true);
        }
    }

    // test
    // debuff
    public void AffectDebuff(DebuffInfo debuffInfo)
    {
        //if (_helper != null)
        //{
        //    if (!_helper.IsCorrectHand())
        //        return;
        //}

        if (isSlow)
        {
            DebuffSlow slow = new DebuffSlow();
            slow.totalTime = 3.0f;

            debuffInfo.AddDebuff(slow);
        }

        if (isPoison)
        {
            DebuffPoison poison = new DebuffPoison();
            poison.totalTime = 5.0f;

            debuffInfo.AddDebuff(poison);
        }

        if (isElectric)
        {
            DebuffElectric electric = new DebuffElectric();
            electric.totalTime = 5.0f;

            debuffInfo.AddDebuff(electric);
        }

        if (isWeakness)
        {
            DebuffWeakness weak = new DebuffWeakness();
            weak.totalTime = 3.0f;

            debuffInfo.AddDebuff(weak);
        }

        if (isRage)
        {
            DebuffRage rage = new DebuffRage();
            rage.totalTime = 5.0f;

            debuffInfo.AddDebuff(rage);
        }
    }
}
