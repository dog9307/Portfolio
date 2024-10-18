using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingArrowCreator : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    private Color[] _colors;

    [SerializeField]
    private GlowUpAndDown _glow;
    
    public MagicArrowUser owner { get; set; }

    [SerializeField]
    private float _chargeOnceTime;
    private float _currentTime;
    public int totalCount { get { if (!owner) return 0; return owner.currentCount; } }

    private int _currentIndex;

    public string skillKey { get; set; }
    public int chargedCount { get { return _currentIndex + 1; } }
    
    public GameObject effectPrefab { get; set; }
    public bool isChargeEnd { get; set; }

    [SerializeField]
    private float _minIntensityIncrease = 0.05f;
    [SerializeField]
    private float _maxIntensityIncrease = 0.1f;

    private PlayerSkillUsage _playerSkill;

    [SerializeField]
    private float _defaultDamage = 7.0f;

    void Start()
    {
        _currentIndex = 0;
        _currentTime = 0.0f;

        _playerSkill = FindObjectOfType<PlayerSkillUsage>();
        // before skill rotation
        //SkillInfo info = _playerSkill.GetComponent<SkillInfo>();
        //int index = -1;
        //for (int i = 0; i < info.activeSkills.Count; ++i)
        //{
        //    if (typeof(MagicArrow).IsInstanceOfType(info.activeSkills[i]))
        //    {
        //        index = i;
        //        break;
        //    }
        //}
        //if (index == -1) return;

        //skillKey = "skill" + ((index % PlayerSkillUsage.HAND_MAX_COUNT) + 1);

        isChargeEnd = false;
    }

    void Update()
    {
        if (isChargeEnd) return;

        Charging();

        // before skill rotation
        //if (!KeyManager.instance.IsStayKeyDown(skillKey))
        // after skill rotation
        if (!KeyManager.instance.IsStayKeyDown("skill_use"))
        {
            if (_playerSkill.isSkillUsing || chargedCount <= 0)
            {
                Destroy(gameObject);
                return;
            }

            ChargeEnd();
        }
    }

    void Charging()
    {
        if (!_glow) return;

        if (_currentTime < _chargeOnceTime)
        {
            _currentTime += IsterTimeManager.deltaTime;
            if (_currentTime >= _chargeOnceTime)
            {
                _currentIndex++;
                _currentTime = 0.0f;
            }
        }

        if (_currentIndex >= totalCount)
            _currentIndex = totalCount - 1;

        int realIndex = (_currentIndex < 0 ? 0 : _currentIndex);
        _glow.color = _colors[realIndex];
        _glow.minIntensity = 1.0f + _minIntensityIncrease * realIndex;
        _glow.maxIntensity = 1.0f + _maxIntensityIncrease * realIndex;
    }

    void ChargeEnd()
    {
        PlayerAnimController player = FindObjectOfType<PlayerAnimController>();
        player.ChargeEnd();

        isChargeEnd = true;
    }

    public GameObject FireBullet(bool isLaser = false)
    {
        GameObject newBullet = CreateObject();
        if (!newBullet) return null;

        float damageMultiplier = owner.damageMultiplier;
        if (!isLaser)
        {
            transform.parent = newBullet.transform;
            transform.localPosition = Vector2.zero;

            MagicArrowController controller = newBullet.GetComponent<MagicArrowController>();
            Vector2 dir = CommonFuncs.CalcDir(owner.transform.position, owner.bulletCreator.transform.position);
            controller.dir = dir;
            //(transform.position - transform.parent.GetComponentInParent<Movable>().center).normalized;
            controller.Init();
        }
        else
        {
            newBullet.transform.position = owner.bulletCreator.transform.position + newBullet.transform.right * 0.5f * chargedCount;
            newBullet.transform.parent = _playerSkill.transform;
            Vector3 scale = newBullet.transform.localScale;
            scale.x *= chargedCount;
            scale.y *= chargedCount;
            newBullet.transform.localScale = scale;

            ChargingLaserController chargingLaser = newBullet.GetComponent<ChargingLaserController>();
            chargingLaser.bulletSpeed = 1.0f / (float)chargedCount;

            damageMultiplier = 1.0f;

            Destroy(gameObject);
        }

        SkillInfo info = GameObject.FindObjectOfType<PlayerSkillUsage>().GetComponent<SkillInfo>();
        MagicArrow skill = info.FindSkill<MagicArrow>();

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
            if (!damager) return null;
        }

        Damage realDamage = damager.damage;
        realDamage.damage = owner.defaultDamage;
                                      // Player Buff   + Skill buff
        realDamage.additionalDamage = additionalDamage + owner.additionalDamage;
        realDamage.damageMultiplier = damageMultiplier * chargedCount;
        realDamage.knockbackFigure = 1.0f;

        damager.damage = realDamage;

        for (int i = 0; i < chargedCount; ++i)
            owner.CoolTimeStart();

        return newBullet;
    }

    public GameObject CreateObject()
    {
        if (!effectPrefab) return null;

        GameObject newBullet = GameObject.Instantiate(effectPrefab);
        newBullet.transform.position = owner.bulletCreator.transform.position;

        Vector2 dir = CommonFuncs.CalcDir(owner.transform.position, owner.bulletCreator.transform.position);
        float angle = CommonFuncs.DirToDegree(dir);
        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        return newBullet;
    }
}
