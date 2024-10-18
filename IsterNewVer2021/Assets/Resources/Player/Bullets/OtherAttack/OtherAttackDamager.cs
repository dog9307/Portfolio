using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherAttackDamager : Damager
{
    public PassiveOtherAttackUser user { get; set; }
    public bool isDebuffDamaging { get; set; }

    protected override void Start()
    {
        if (NormalAttackDamager.isXFlip)
        {
            Vector3 angle = transform.localEulerAngles;
            angle.x = 180.0f;
            transform.localEulerAngles = angle;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            //Movable owner = user.transform.parent.GetComponentInParent<Movable>();
            Movable owner = FindObjectOfType<PlayerMoveController>();
            // 방향 보정
            Vector2 ownerToBullet = CommonFuncs.CalcDir(owner, this);
            Vector2 ownerToEnemy = CommonFuncs.CalcDir(owner, collision);
            Vector2 dir = (ownerToBullet + ownerToEnemy).normalized;

            Damage realDamage = damage;
            //if (isDebuffDamaging)
            //{
            //    DebuffInfo debuffInfo = damagable.GetComponent<DebuffInfo>();
            //    if (debuffInfo)
            //        realDamage.damageMultiplier *= (1.5f + 0.1f * user.level * debuffInfo.abnormalCount);
            //}

            damagable.HitDamager(realDamage, dir);
        }
    }
}
