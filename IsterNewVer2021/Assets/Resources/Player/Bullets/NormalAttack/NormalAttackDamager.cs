using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackDamager : Damager
{
    private BuffInfo _buff;
    public SkillUserManager skillUserManager { get; set; }

    public static bool isXFlip;

    protected override void Start()
    {
        base.Start();

        if (!skillUserManager) return;

        _buff = skillUserManager.GetComponentInParent<BuffInfo>();
        
        if (isXFlip)
        {
            Vector3 angle = transform.localEulerAngles;
            angle.x = 180.0f;
            transform.localEulerAngles = angle;
        }
    }

    private void OnDestroy()
    {
        if (!_buff) return;

        if (_buff.isRangeAttackOn)
            RangeAttack();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_owner.ToString().Equals(collision.tag)) return;
        
        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            Movable owner = skillUserManager.GetComponentInParent<Movable>();
            // 방향 보정
            Vector2 ownerToBullet = CommonFuncs.CalcDir(owner, this);
            Vector2 ownerToEnemy = CommonFuncs.CalcDir(owner, collision);
            Vector2 dir = (ownerToBullet + ownerToEnemy).normalized;

            Damage realDamage = damage;
            realDamage.additionalDamage += _buff.additionalDamage;

            PassiveRemoverUser remover = skillUserManager.FindUser(typeof(PassiveRemover)) as PassiveRemoverUser;
            if (remover)
            {
                if (remover.isStackDamaging)
                    realDamage.additionalDamage += remover.stackCount;
                if (remover.isStackHealing)
                {
                    Damagable player = _buff.GetComponent<Damagable>();
                    if (player)
                    {
                        Damage heal = DamageCreator.Create(gameObject, -remover.stackCount, 0.0f, 1.0f, 0.0f);
                        player.HitDamager(heal, Vector2.zero);
                    }
                }
                remover.stackCount = 0;
            }

            if (typeof(MirageBuffInfo).IsInstanceOfType(_buff))
            {
                ActiveMirageUser activeMirage = FindObjectOfType<ActiveMirageUser>();
                if (activeMirage)
                    realDamage.isMarkerDestroy = activeMirage.isMarkerDestroy;
            }

            damagable.HitDamager(realDamage, dir);
            
            if (typeof(PassiveMirageController).IsInstanceOfType(owner))
            {
                if (!((PassiveMirageController)owner).isSkillUse)
                    return;
            }

            if (_buff.isTracingArrowOn)
                CreateTracingArrow(collision);
            if (_buff.isPassiveReversePowerOn)
                PassiveReversePowerOn();
            if (_buff.isCoolTimePassiveOn)
                CoolTimeDown(collision.bounds.center, collision);
            if (_buff.isMarkerOn)
                Marking(collision.gameObject);
            if (_buff.knockbackIncrease > 0.0f)
                DragDown(collision.gameObject);

            AffectDebuff(collision.gameObject);
            ForcedCounterAttack(collision.gameObject);
        }
    }

    public void CreateTracingArrow(Collider2D target)
    {
        TracingArrowUser user = skillUserManager.FindUser(typeof(TracingArrow)) as TracingArrowUser;
        if (!user) return;

        user.target = target;
        skillUserManager.UseSkill(typeof(TracingArrow));
    }

    public void PassiveReversePowerOn()
    {
        PassiveReversePowerUser user = skillUserManager.FindUser(typeof(PassiveReversePower)) as PassiveReversePowerUser;
        if (!user) return;

        user.isPassiveReversePowerOn = _buff.isPassiveReversePowerOn;
        skillUserManager.UseSkill(typeof(PassiveReversePower));
    }

    public void CoolTimeDown(Vector2 position, Collider2D collision)
    {
        PassiveCoolTimeDownUser user = skillUserManager.FindUser(typeof(PassiveCoolTimeDown)) as PassiveCoolTimeDownUser;
        if (!user) return;

        user.startPos = position;
        skillUserManager.UseSkill(typeof(PassiveCoolTimeDown));

        DebuffInfo debuffInfo = collision.GetComponent<DebuffInfo>();
        if (debuffInfo)
            user.DebuffAdditiveCootTimeDown(debuffInfo);

        if (user.isMirage)
        {
            ActiveMirageUser activeMirage = FindObjectOfType<ActiveMirageUser>();
            if (activeMirage)
            {
                if (activeMirage.isAllBuff)
                    activeMirage.AllBuffOn();
            }
        }
    }

    public void RangeAttack()
    {
        PassiveRangeAttackBuffUser user = skillUserManager.FindUser(typeof(PassiveRangeAttackBuff)) as PassiveRangeAttackBuffUser;
        if (!user) return;

        user.UseSkill();
    }

    public void Marking(GameObject enemy)
    {
        PassiveMarkerUser user = skillUserManager.FindUser(typeof(PassiveMarker)) as PassiveMarkerUser;
        if (!user) return;

        user.targetEnemy = enemy;
        user.UseSkill();
    }

    public void DragDown(GameObject enemy)
    {
        PassiveKnockbackIncreaseUser user = skillUserManager.FindUser(typeof(PassiveKnockbackIncrease)) as PassiveKnockbackIncreaseUser;
        if (!user) return;

        user.target = enemy;
        user.UseSkill();
    }

    public void AffectDebuff(GameObject enemy)
    {
        AdditionalAttackUser user = skillUserManager.FindUser(typeof(AdditionalAttack)) as AdditionalAttackUser;
        if (!user) return;

        DebuffInfo debuffInfo = enemy.GetComponent<DebuffInfo>();
        if (!debuffInfo) return;

        user.AffectDebuff(debuffInfo);
    }

    public void ForcedCounterAttack(GameObject enemy)
    {
        PassiveCounterAttackUser user = skillUserManager.FindUser(typeof(PassiveCounterAttack)) as PassiveCounterAttackUser;
        if (!user) return;
        if (!user.isMirage) return;

        ActiveMirageUser activeMirage = FindObjectOfType<ActiveMirageUser>();
        if (activeMirage)
        {
            if (activeMirage.isForcedCounterAttack)
            {
                CanCounterAttackedObject counter = enemy.GetComponent<CanCounterAttackedObject>();
                if (!counter) return;

                counter.TimerStart();
            }
        }
    }
}
