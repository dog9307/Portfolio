using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCounterAttackUser : SwordChangingUser, IObjectCreator
{
    [SerializeField]
    private float _maxDistance = 3.0f;
    public float maxDistance { get { return (_maxDistance + additionalRange); } }

    //public HAND relativeHand { get; set; }

    [SerializeField]
    private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    public int maxCount { get; set; }

    public float additionalDamage { get; set; }
    public float additionalMultiplier { get; set; }

    public float additionalRange { get; set; }

    private CounterAttackUserHelperBase _currentHelper;
    public T GetHelper<T>() where T : CounterAttackUserHelperBase
    {
        return (T)_currentHelper;
    }

    public bool HelperCheck(System.Type type)
    {
        return type.IsInstanceOfType(_currentHelper);
    }

    //public class CounterAttackUserHandChanger : SkillUserHandChanger<PassiveCounterAttack>
    //{
    //    public override void BuffOn()
    //    {
    //        _buff.isCanCounterAttack = true;
    //    }

    //    public override void BuffOff()
    //    {
    //        _buff.isCanCounterAttack = false;
    //    }
    //}

    protected override void Start()
    {
        base.Start();

        //DualSwordSetting<CounterAttackUserHandChanger>();

        PlayerSkillUsage playerSkills = FindObjectOfType<PlayerSkillUsage>();
        //relativeHand = playerSkills.GetSkillHand<PassiveCounterAttack>();

        ApplyHelper(new NormalCounterAttackHelper());

        damageMultiplier = 1.0f;
        additionalMultiplier = 0.0f;
        additionalDamage = 0.0f;

        additionalRange = 0.0f;

        maxCount = 1;
        _buff.isCanCounterAttack = true;

        SwordChange();
        _attack.ChangeHelper(new PlayerDashAttackHelper());

        //PlayerMoveController move = playerSkills.GetComponent<PlayerMoveController>();
        //transform.position = move.center;
    }

    public bool IsCanCounterAttack(HAND clickedHand)
    {
        //if (relativeHand != clickedHand) return false;
        if (_currentHelper == null) return false;

        return _currentHelper.IsCanCounterAttack();
    }

    public override void UseSkill()
    {
        if (_currentHelper == null) return;

        PlayerMoveController player = transform.parent.GetComponentInParent<PlayerMoveController>();

        Vector2 currentPos = player.center;
        Vector2 nextPos = _currentHelper.currentTarget.transform.position;
        Vector2 dir = CommonFuncs.CalcDir(currentPos, nextPos);
        nextPos = nextPos + dir * 2.0f;

        player.Move(nextPos);

        LookAtMouse look = transform.parent.GetComponentInParent<LookAtMouse>();
        look.dir = dir;

        CanCounterAttackedObject target = _currentHelper.currentTarget.GetComponent<CanCounterAttackedObject>();
        target.isCanCountered = false;

        GameObject newBullet = CreateObject();
        _currentHelper.UseSkill(newBullet, dir);
    }
    
    public void ApplyHelper(CounterAttackUserHelperBase helper)
    {
        if (_currentHelper != null)
            _currentHelper.Release();

        _currentHelper = helper;
        _currentHelper.owner = this;
        _currentHelper.Init();
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab);
        newBullet.transform.position = _currentHelper.currentTarget.transform.position;

        return newBullet;
    }

    public void TimerStart()
    {
        if (!HelperCheck(typeof(AdvancedCounterAttaclHelper))) return;

        ((AdvancedCounterAttaclHelper)_currentHelper).timer.TimerStart();
    }

    public void CounterAttackReset()
    {
        if (!HelperCheck(typeof(AdvancedCounterAttaclHelper))) return;

        ((AdvancedCounterAttaclHelper)_currentHelper).CounterAttackReset();

        additionalMultiplier = 0.0f;
        additionalRange = 0.0f;
    }
}
