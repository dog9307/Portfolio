using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeDownUser : SkillUser, IObjectCreator
{
    public Vector2 startPos { get; set; }
    public GameObject effectPrefab { get; set; }

    public float attackSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float criticalPercentage { get; set; }

    public bool isDefeatCoolTime { get; set; }
    public bool isDebuffAdditive { get; set; }

    private bool _isCoolTimeMode;
    public bool isCoolTimeMode
    {
        get { return _isCoolTimeMode; }
        set
        {
            _isCoolTimeMode = value;
            if (_isCoolTimeMode)
                _currentCoolTime = _totalCoolTime;
        }
    }
    [SerializeField]
    private float _totalCoolTime = 10.0f;
    public float totalCoolTime { get { return _totalCoolTime; } }
    private float _currentCoolTime;
    public float currentCoolTime { get { return _currentCoolTime; } }

    private PlayerSkillUsage _playerSkills;
    private PassiveCoolTimeDownUser _playerUser;

    public bool isRandomReset { get; set; }
    public bool isCanUseSkill
    {
        get
        {
            if (!_isCoolTimeMode)
                return true;
            else
            {
                if (!_playerUser)
                    _playerUser = _playerSkills.GetComponentInChildren<SkillUserManager>().FindUser(typeof(PassiveCoolTimeDown)) as PassiveCoolTimeDownUser;

                return (_playerUser._currentCoolTime >= _playerUser._totalCoolTime);
            }
        }
    }

    //public class CoolTimeDownUserHandChanger : SkillUserHandChanger<PassiveCoolTimeDown>
    //{
    //    public PlayerAttacker attack { get; set; }
    //    public PassiveCoolTimeDownUser owner { get; set; }

    //    public override void BuffOn()
    //    {
    //        _buff.isCoolTimePassiveOn = true;
    //        _buff.buffSpeedUp += owner.moveSpeed;
    //        _buff.criticalPercentage += owner.criticalPercentage;
    //        if (attack)
    //            attack.attackSpeedMultiplier = owner.attackSpeed;
    //    }

    //    public override void BuffOff()
    //    {
    //        _buff.isCoolTimePassiveOn = true;
    //        _buff.buffSpeedUp -= owner.moveSpeed;
    //        if (_buff.buffSpeedUp < 0.0f)
    //            _buff.buffSpeedUp = 0.0f;

    //        _buff.criticalPercentage -= owner.criticalPercentage;
    //        if (_buff.criticalPercentage < 0.0f)
    //            _buff.criticalPercentage = 0.0f;

    //        if (attack)
    //            attack.attackSpeedMultiplier = 1.0f;
    //    }
    //}

    public int figureLevel { get; set; }

    protected override void Start()
    {
        base.Start();

        effectPrefab = Resources.Load<GameObject>("Player/Skills/Effects/CoolTimeDown/CoolTimeDown");

        //DualSwordSetting<CoolTimeDownUserHandChanger>();

        //if (_helper != null)
        //{
        //    PlayerAttacker attack = _info.GetComponent<PlayerAttacker>();
        //    ((CoolTimeDownUserHandChanger)_helper).attack = attack;
        //    ((CoolTimeDownUserHandChanger)_helper).owner = this;
        //}

        _playerSkills = FindObjectOfType<PlayerSkillUsage>();
        _playerUser = _playerSkills.GetComponentInChildren<SkillUserManager>().FindUser(typeof(PassiveCoolTimeDown)) as PassiveCoolTimeDownUser;

        attackSpeed = 1.0f;

        isCoolTimeMode = false;
    }
    
    void Update()
    {
        //if (_helper == null) return;

        //_helper.Update();

        if (isCoolTimeMode)
            CoolTimeUpdate();
    }

    void CoolTimeUpdate()
    {
        if (isCanUseSkill) return;

        _currentCoolTime += IsterTimeManager.deltaTime;
    }

    public GameObject CreateObject()
    {
        GameObject effect = Instantiate(effectPrefab);
        effect.transform.position = startPos;

        return effect;
    }

    float CalcFigure(PassiveCoolTimeDown skill, ICoolTime targetSkill, float multiplier)
    {
        if (!isCoolTimeMode)
            return targetSkill.totalCoolTime * skill.figure * (figureLevel + 1) * multiplier;

        return 10.0f * (figureLevel + 1);
    }

    public void CoolTimeDown(bool isCreateEffect, float multiplier = 1.0f)
    {
        if (!isCanUseSkill) return;

        if (isCreateEffect)
            CreateObject();

        SkillInfo info = transform.parent.GetComponentInParent<SkillInfo>();
        PassiveCoolTimeDown skill = info.FindSkill<PassiveCoolTimeDown>();

        if (!_playerSkills)
            _playerSkills = FindObjectOfType<PlayerSkillUsage>();

        //List<ActiveBase> activeSkills = null;
        //if (_helper != null)
        //    activeSkills = _playerSkills.FindSkills<ActiveBase>(_helper.relativeHand, skill.otherType);
        //else
        //    activeSkills = _playerSkills.FindSkills<ActiveBase>(skill.otherType);

        List<ActiveUserBase> activeUsers = _playerSkills.FindUsers<ActiveUserBase>();
        foreach (var user in activeUsers)
        {
            if (user.IsCoolTime())
            {
                float figure = CalcFigure(skill, user, multiplier);
                user.CoolTimeDown(figure);
            }
        }

        if (isCoolTimeMode)
        {
            if (!_playerUser)
                _playerUser = _playerSkills.GetComponentInChildren<SkillUserManager>().FindUser(typeof(PassiveCoolTimeDown)) as PassiveCoolTimeDownUser;

            _playerUser._currentCoolTime = 0.0f;
        }
    }

    public override void UseSkill()
    {
        CoolTimeDown(true);

        if (isRandomReset)
        {
            List<ActiveUserBase> randomList = _playerSkills.FindUsers<ActiveUserBase>();

            int rnd = Random.Range(0, randomList.Count);
            
            ActiveUserBase active = randomList[rnd];
            active.CoolTimeDown(active.totalCoolTime);
        }
    }

    public void DefeatCoolTimeDown()
    {
        if (!isCanUseSkill) return;
        
        CoolTimeDown(true);
    }

    public void DebuffAdditiveCootTimeDown(DebuffInfo debuffInfo)
    {
        if (!isDebuffAdditive) return;

        CoolTimeDown(false, 0.5f);
    }

    public void AttackSpeedUp(bool isUp)
    {
        float figure = 1.0f;
        List<SkillBase> passiveList = _info.passiveSkills;
        //if (_helper != null)
        //    passiveList = _info.FindSkills<PassiveBase>(_helper.relativeHand);
        //else
        //    passiveList = _info.FindSkills<PassiveBase>();

        figure += 0.2f * passiveList.Count;

        if (!isUp)
            figure = 1.0f / figure;

        attackSpeed *= figure;
        if (attackSpeed < 1.0f)
            attackSpeed = 1.0f;

        PlayerAttacker attack = _info.GetComponent<PlayerAttacker>();
        if (!attack) return;

        //if (_helper != null)
        //{
        //    if (_helper.IsCorrectHand())
        //        attack.attackSpeedMultiplier = attackSpeed;
        //}
        //else
            attack.attackSpeedMultiplier = attackSpeed;
    }


    public void MoveSpeedUp(bool isUp)
    {
        float figure = 0.0f;
        List<SkillBase> passiveList = _info.passiveSkills;
        //if (_helper != null)
        //    passiveList = _info.FindSkills<PassiveBase>(_helper.relativeHand);
        //else
        //    passiveList = _info.FindSkills<PassiveBase>();

        figure += 0.5f * passiveList.Count;

        if (!isUp)
            figure *= -1.0f;

        moveSpeed += figure;
        if (moveSpeed < 0.0f)
            moveSpeed = 0.0f;

        //if (_helper != null)
        //{
        //    if (_helper.IsCorrectHand())
        //        _buff.buffSpeedUp = moveSpeed;
        //}
        //else
            _buff.buffSpeedUp = moveSpeed;
    }

    public void CriticalUp(bool isUp)
    {
        float figure = 0.0f;
        List<SkillBase> passiveList = _info.passiveSkills;
        //if (_helper != null)
        //    passiveList = _info.FindSkills<PassiveBase>(_helper.relativeHand);
        //else
        //    passiveList = _info.FindSkills<PassiveBase>();

        figure += 0.1f * passiveList.Count;

        if (!isUp)
            figure *= -1.0f;

        criticalPercentage += figure;
        if (criticalPercentage < 0.0f)
            criticalPercentage = 0.0f;

        //if (_helper != null)
        //{
        //    if (_helper.IsCorrectHand())
        //        _buff.criticalPercentage = criticalPercentage;
        //}
        //else
            _buff.criticalPercentage = criticalPercentage;
    }
}
