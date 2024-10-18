using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LookAtMouse))]
[RequireComponent(typeof(PlayerAttacker))]
[RequireComponent(typeof(PlayerMoveController))]
[RequireComponent(typeof(PlayerDashDelay))]
[RequireComponent(typeof(Damagable))]
public class PlayerAnimController : AnimController
{
    private LookAtMouse _look;
    private PlayerAttacker _attack;
    private PlayerMoveController _move;
    private PlayerDashDelay _delay;
    private Damagable _damagable;

    protected override void Awake()
    {
        base.Awake();

        _look = GetComponent<LookAtMouse>();
        _attack = GetComponent<PlayerAttacker>();
        _move = GetComponent<PlayerMoveController>();
        _delay = GetComponent<PlayerDashDelay>();
        _damagable = GetComponent<Damagable>();
    }

    void Update()
    {
        // test
        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    Damage damage = DamageCreator.Create(50.0f, 0.0f, 1.0f);
        //    GetComponent<Damagable>().HitDamager(damage, Vector2.right);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha8))
        //{
        //    Damage damage = DamageCreator.Create(10.0f, 0.0f, 1.0f);
        //    GetComponent<Damagable>().HitDamager(damage, Vector2.right);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha9))
        //{
        //    Damage damage = DamageCreator.Create(20.0f, 0.0f, 1.0f);
        //    GetComponent<Damagable>().HitDamager(damage, Vector2.right);
        //}
        
        _anim.SetBool("isHurt", _damagable.isHurt);
        _anim.SetBool("isKnockback", _damagable.isKnockback);
        _anim.SetFloat("velocity", _move.targetVelocity.magnitude);

        if (_move.isMoving)
        {
            //Vector2 dir = _move.velocity.normalized;
            //_anim.SetFloat("dirX", dir.x);
            //_anim.SetFloat("dirY", dir.y);
            Vector2 dir = _move.keyDir;
            _anim.SetFloat("dirX", dir.x);
            _anim.SetFloat("dirY", dir.y);
        }
        //else
        //{
        //    _anim.SetFloat("dirX", _look.dir.x);
        //    _anim.SetFloat("dirY", _look.dir.y);
        //}
        _anim.SetBool("isAttack", _attack.isAttacking);
        _anim.SetInteger("attackType", (int)_attack.attackType);
        _anim.SetFloat("hand", (float)_attack.currentHand);
        _anim.SetBool("isDashAttack", _attack.isDashAttack);

        _anim.SetBool("isDelay", _delay.isDelay);
        _anim.SetFloat("attackMultiplier", _attack.realMultiplier * IsterTimeManager.globalTimeScale);
        _anim.SetBool("isDie", _damagable.isDie);

        _anim.SetFloat("battleMultiplier", (_attack.isBattle ? 1.5f : 1.0f) * IsterTimeManager.globalTimeScale);

        _anim.SetFloat("timeMultiplier", IsterTimeManager.globalTimeScale);
    }

    public void ChangeCharacter(RuntimeAnimatorController controller)
    {
        _anim.runtimeAnimatorController = controller;
    }

    public void AttackCharging(bool isStart)
    {
        _anim.SetBool("attackCharging", isStart);

        LookMouseDir();
    }

    public void Parrying(bool isSuccess)
    {
        _anim.SetBool("isParryingSuccess", isSuccess);
    }

    public void ChargeAttack()
    {
        _anim.SetTrigger("chargeAttack");
    }
    
    public void UseActiveSkill(int skill)
    {
        _anim.SetTrigger("useSkill");
        _anim.SetInteger("skillKind", skill);
    }

    public void Charging(bool isCharging)
    {
        MagicArrowUser user = GetComponent<PlayerSkillUsage>().FindUser<MagicArrow>() as MagicArrowUser;
        user.isCanCharge = isCharging;

        _anim.SetBool("isCharging", isCharging);
    }

    public void ChargeEnd()
    {
        MagicArrowUser user = GetComponentInChildren<SkillUserManager>().FindUser(typeof(MagicArrow)) as MagicArrowUser;
        ChargingArrowCreator charging = user.GetComponentInChildren<ChargingArrowCreator>();
        if (!charging) return;

        _anim.SetTrigger("chargingEnd");
    }

    public void SkillEnd()
    {
        _anim.SetTrigger("skillEnd");
    }

    public override void Die()
    {
        base.Die();

        _move.PlayerMoveFreeze(true);

        _anim.SetBool("isFalling", false);

        Undamagable undamagable = GetComponent<Undamagable>();
        if (undamagable)
            undamagable.StopUndamagable();

        WaterFX water = FindObjectOfType<WaterFX>();
        if (water)
            water.enabled = false;
    }

    public void LookMouseDir()
    {
        _anim.SetFloat("dirX", _look.dir.x);
        _anim.SetFloat("dirY", _look.dir.y);
    }

    public void Falling()
    {
        _anim.SetBool("isFalling", true);
    }

    [SerializeField]
    private GameObject _reflection;
    public void ReflectionOn(bool isOn)
    {
        _reflection.SetActive(isOn);
    }

    public void PlayerStayInteract(bool isStay)
    {
        _anim.SetBool("isStayInteract", isStay);
    }

    public void ResetTrigger(string name)
    {
        _anim.ResetTrigger(name);
    }
}
