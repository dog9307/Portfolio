using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : Movable
{
    private PlayerAttacker _attack;
    private PlayerDashDelay _delay;
    private PlayerSkillUsage _skill;
    private BuffInfo _buff;

    [SerializeField]
    private SFXPlayer _sfx;

    public override float speed
    {
        get
        {
            float speed = _speed;
            if (_buff)
                speed = (speed + _buff.additionalSpeed) * _buff.speedMultiplier;

            if (_debuffInfo)
                speed *= _debuffInfo.slowMultiplier;

            return speed;
        }
    }

    public bool isAttacking { get { if (_attack) return _attack.isAttacking; else return false; } }
    public bool isDelay { get { if (_delay) return _delay.isDelay; else return false; } }
    public bool isSkillUsing { get { if (_skill) return _skill.isSkillUsing; else return false; } }
    public bool isDash { get; set; }

    private Vector2 _dashStartPos;
    public Vector2 dashStartPos
    {
        get { return _dashStartPos; }
        set
        {
            Collider2D[] cols = Physics2D.OverlapPointAll(center);
            foreach (var col in cols)
            {
                if (col.gameObject.layer == LayerMask.NameToLayer("Room") && col.enabled)
                {
                    _dashStartPos = value;
                    break;
                }
            }
        }
    }

    public override bool isHide
    {
        get { return base.isHide; }
        set
        {
            base.isHide = value;
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Enemys"), value);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Bullets"), value);
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Gumddak"), value);
        }
    }

    public bool isMoving
    {
        get
        {
            bool isMove = false;

            isMove = (_rigid.velocity.magnitude > 0.0f) && IsCanMoveWithDamagable() && IsCanMove();

            return isMove;
        }
    }

    public bool IsCanMove()
    {
        bool isCanMove = !GetComponent<Attacker>().isAttacking && !GetComponent<PlayerSkillUsage>().isSkillUsing;

        return isCanMove;
    }

    public Vector2 velocity
    {
        get
        {
            return _rigid.velocity;
        }
    }

    public Vector2 additionalDir { get; set; }
    public Vector2 keyDir { get; set; }

    void Awake()
    {
        _attack = GetComponent<PlayerAttacker>();
        _delay = GetComponent<PlayerDashDelay>();
        _skill = GetComponent<PlayerSkillUsage>();
        _buff = GetComponent<BuffInfo>();

        //int swordCount = PlayerPrefs.GetInt("swordCount", 0);
        //if (swordCount == 0)
        //{
        //    //PlayerMoveFreeze(true);
        //    GetComponent<LookAtMouse>().dir = Vector2.down;
        //}
    }

    public void Move(Vector3 position)
    {
        transform.position = position;
        //Cinemachine.CinemachineBrain brain = FindObjectOfType<Cinemachine.CinemachineBrain>();
        //if (brain)
        //    brain.transform.position = position;

        //if (Camera.main)
        //{
        //    SkipBlend skip = Camera.main.GetComponent<SkipBlend>();
        //    if (skip)
        //        skip.isSkip = true;
        //}
    }

    protected override void ComputeVelocity()
    {
        PassiveChargeAttackUser chargeUser = _skill.chargeUser;
        if (chargeUser)
        {
            if (chargeUser.isChargingStart)
                return;
        }

        bool isCantMoveWithState = isAttacking || isDelay || isSkillUsing || isDash;
        Vector2 dir = Vector2.zero;
        if (isCantMoveWithState)
            dir = _attack.dir;
        else
        {
            if (KeyManager.instance.IsStayKeyDown("left"))
                dir += Vector2.left;

            if (KeyManager.instance.IsStayKeyDown("right"))
                dir += -Vector2.left;

            if (KeyManager.instance.IsStayKeyDown("up"))
                dir += Vector2.up;

            if (KeyManager.instance.IsStayKeyDown("down"))
                dir += -Vector2.up;
        }

        keyDir = dir.normalized;

        if (additionalDir.magnitude > 0.0f && Mathf.Abs(dir.x) > 0.0f) 
        {
            float dot = Vector2.Dot(dir, additionalDir);

            if (dot > 0.0f)
            {
                if (Mathf.Abs(dir.y) > 0.0f)
                    dir += additionalDir;
                else
                    dir = additionalDir;
            }
            else if (dot < 0.0f)
            {
                if (Mathf.Abs(dir.y) > 0.0f)
                    dir -= additionalDir;
                else
                    dir = -additionalDir;
            }
        }

        dir = dir.normalized;

        _targetVelocity = dir * (isCantMoveWithState ? _attack.speed : speed) * IsterTimeManager.globalTimeScale;
    }

    public void PlayerMoveFreeze(bool isFreeze)
    {
        KeyManager.instance.isEnable = !isFreeze;
        GetComponent<LookAtMouse>().isEnable = !isFreeze;
        if (isFreeze)
        {
            _targetVelocity = Vector2.zero;
            _rigid.velocity = _targetVelocity;
        }
        //GetComponentInChildren<CameraFallow>().enabled = !isFreeze;
    }

    [SerializeField]
    private GameObject _fallingEffect;
    [SerializeField]
    private TestPlayerEquipment _playerEquip;
    public void Falling(float damage)
    {
        if (_fallingEffect)
        {
            GameObject effect = Instantiate(_fallingEffect);
            effect.transform.position = transform.position;
            effect.GetComponent<ParticleColorChanger>().ColorChange(_playerEquip.currentAttackColor);
        }

        _damagable.isCanHurt = true;
        _damagable.HitDamager(DamageCreator.Create(gameObject.gameObject, damage, 0.0f, 1.0f), Vector2.zero);
        Move(dashStartPos);
        PlaySFX("falling");

        _damagable.ResetState();
    }

    public void PlaySFX(string name)
    {
        if (_sfx)
            _sfx.PlaySFX(name);
    }
}
