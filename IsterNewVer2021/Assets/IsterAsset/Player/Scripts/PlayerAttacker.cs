using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HAND
{
    NONE = -1,
    LEFT,
    RIGHT,
    END
}

public enum ATTACK_TYPE
{
    NONE = -1,
    NORMAL,
    COUNTER,
    END
}

[RequireComponent(typeof(LookAtMouse))]
[RequireComponent(typeof(PlayerMoveController))]
[RequireComponent(typeof(PlayerDashDelay))]
[RequireComponent(typeof(PlayerSkillUsage))]
public class PlayerAttacker : Attacker
{
    [SerializeField]
    private GameObject _afterImage;

    private LookAtMouse _mouse;
    private PlayerMoveController _move;
    private PlayerDashDelay _delay;
    public PlayerDashDelay delay { get { return _delay; } }

    private PlayerSkillUsage _skill;
    public PlayerSkillUsage skill { get { return _skill; } }

    private BuffInfo _buff;
    private Damagable _damagable;
    private Undamagable _undamagable;

    private NormalBulletCreator _normal;

    public Vector2 dir { get; set; }

    [SerializeField]
    private float _dashMultiplier;
    public float GetDashMultiplier() { return _dashMultiplier; }
    [SerializeField]
    private float _attackMultiplier;
    public float GetAttackMultiplier() { return _attackMultiplier; }
    private float _realMultiplier;
    public float attackSpeedMultiplier { get; set; }
    public float realMultiplier { get { return _realMultiplier * attackSpeedMultiplier; } set { _realMultiplier = value; } }
    private float _speed;
    public float startSpeed { get { return _speed * _speedMultiplier; } }
    public float speed { get { return _speed * _currentSpeedMultiplier * _buff.speedMultiplier; } }

    [SerializeField]
    private float _speedMultiplier;
    private float _currentSpeedMultiplier;
    public float speedMultiplier { get { return _currentSpeedMultiplier; } set { _currentSpeedMultiplier = value; } }

    public Vector2 attackStartPos { get; set; }

    public HAND currentHand { get; set; }

    private bool _isDualSword;
    public bool isDualSword
    {
        get { return _isDualSword; }

        set
        {
            _isDualSword = value;
            if (_isDualSword)
                DualSwordOn();
            else
                DualSwordOff();
        }
    }

    [SerializeField]
    private GameObject _dashDamager;
    private SkillUserManager _userManager;
    private PassiveCounterAttackUser _counterAttack;
    private ATTACK_TYPE _nextAttackType;
    public ATTACK_TYPE attackType { get { return _nextAttackType; } }

    [SerializeField]
    private TestPlayerEquipment _equipment;
    public TestPlayerEquipment equipment { get { return _equipment; } }
    public bool isBattle { get; set; }

    public bool isDashAttack { get; set; }

    private PlayerAttackHelperBase _currentHelper;

    void Start()
    {
        _mouse = GetComponent<LookAtMouse>();
        _move = GetComponent<PlayerMoveController>();
        _delay = GetComponent<PlayerDashDelay>();
        _skill = GetComponent<PlayerSkillUsage>();
        _damagable = GetComponent<Damagable>();
        _undamagable = GetComponent<Undamagable>();
        _buff = GetComponent<BuffInfo>();

        _normal = GetComponentInChildren<NormalBulletCreator>();

        _speed = _move.GetSpeed();

        _userManager = GetComponentInChildren<SkillUserManager>();

        attackSpeedMultiplier = 1.0f;

        DamagerTurnOn(false);

        int swordCount = PlayerPrefs.GetInt("swordCount", 0);
        if (swordCount == 0)
        {
            KeyManager.instance.Disable("attack_left");
            KeyManager.instance.Disable("tabUI");

            _equipment.gameObject.SetActive(false);
        }
        else
        {
            KeyManager.instance.Enable("attack_left");
            KeyManager.instance.Enable("tabUI");
        }

        KeyManager.instance.Enable("menu");

        isDashAttack = false;

        ChangeHelper(new PlayerNormalAttackHelper());
    }

    public void MultiplierIncrease()
    {
        _realMultiplier = _attackMultiplier;

        _currentSpeedMultiplier = 0.0f;

        HideEnd();

        CreateBullet();

        if (_equipment)
            _equipment.Attack();
    }

    public void HideEnd()
    {
        _move.isHide = false;

        if (!_undamagable.isUndamagable)
        {
            EventTimer timer = GetComponent<EventTimer>();
            if (!timer)
                _damagable.isCanHurt = true;
        }
    }

    void DamagerTurnOn(bool isTurnOn)
    {
        //if (_dashDamager)
        //    _dashDamager.SetActive(isTurnOn);
    }

    public override void AttackStart()
    {
        if (_currentHelper != null)
            _currentHelper.AttackStart();
    }

    public void CounterAttack()
    {
        //EditorApplication.isPaused = true;

        _realMultiplier = _dashMultiplier;

        attackStartPos = transform.position;

        _move.isHide = true;
        
        if (!_undamagable.isUndamagable)
            _damagable.isCanHurt = false;

        _counterAttack.UseSkill();
    }

    public void NormalAttack()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 mousePos = Input.mousePosition;
        dir = (mousePos - screenPos).normalized;
        _mouse.dir = dir;

        _currentSpeedMultiplier = _speedMultiplier;

        _realMultiplier = _dashMultiplier;

        attackStartPos = transform.position;

        AfterImageControl(true);

        _move.isHide = true;

        if (!_undamagable.isUndamagable)
            _damagable.isCanHurt = false;

        DamagerTurnOn(true);
    }

    public override void AttackEnd()
    {
        if (_currentHelper != null)
            _currentHelper.AttackEnd();
    }

    public override bool IsTriggered()
    {
        if (_currentHelper == null) return false;

        return _currentHelper.IsTriggered();
    }

    public void AttackTriggered(ATTACK_TYPE type, HAND hand)
    {
        _nextAttackType = type;
        currentHand = hand;

        SwordAppear();
    }

    public void SwordAppear()
    {
        if (_equipment)
        {
            _equipment.gameObject.SetActive(true);
            _equipment.Appear();
        }
    }

    public bool IsCounterTriggered(HAND clickedHand)
    {
        ActiveShieldUser shield = _userManager.FindUser(typeof(ActiveShield)) as ActiveShieldUser;
        if (shield)
        {
            if (shield.isSkillUsing)
                return false;
        }

        if (!_counterAttack)
        {
            _counterAttack = _userManager.FindUser(typeof(PassiveCounterAttack)) as PassiveCounterAttackUser;

            if (!_counterAttack)
                return false;
        }

        if (_equipment)
            _equipment.Appear();

        return _counterAttack.IsCanCounterAttack(clickedHand);
    }

    public void SkillCancle()
    {
        // 스킬 캔슬
        List<SkillUser> exceptList = new List<SkillUser>();
        exceptList.Add(_userManager.FindUser(typeof(ActiveTimeSlow)));

        _userManager.SkillEndAll(exceptList);
        _skill.isSkillUsing = false;
    }

    public bool IsCanAttack()
    {
        return (!_isAttacking && !_damagable.isHurt && !_damagable.isDie && !_damagable.isKnockback);
    }

    public override void CreateBullet()
    {
        _normal.CreateObject();

        List<SkillBase> skills = _skill.passiveSkills;
        for (int i = 0; i < skills.Count; ++i)
        {
            if (typeof(AttackPassive).IsInstanceOfType(skills[i]))
            {
                skills[i].isReversePowerUp = _skill.isActiveReversePowerOn;
                FindObjectOfType<PlayerEffectManager>().EffectOff("ActiveReversePower");
                skills[i].UseSkill();
            }
        }
        _skill.isActiveReversePowerOn = false;

        DamagerTurnOn(false);
    }

    public void DualSwordOn()
    {
        if (KeyManager.instance)
            KeyManager.instance.Enable("attack_right");
    }

    public void DualSwordOff()
    {
        if (KeyManager.instance)
            KeyManager.instance.Disable("attack_right");
    }

    public void AfterImageControl(bool isOn)
    {
        if (_afterImage)
            _afterImage.SetActive(isOn);
    }

    public void ChangeHelper(PlayerAttackHelperBase nextHelper)
    {
        if (_currentHelper != null)
            _currentHelper.Release();

        _currentHelper = nextHelper;

        _currentHelper.attacker = this;
        _currentHelper.Init();
    }

    public void SwordChange(int swordType)
    {
        if (!_equipment) return;

        _equipment.ChangeSword(swordType);
    }

    public void Appear()
    {
        _equipment.isAttacking = true;
        _equipment.Appear(false);
    }
}
