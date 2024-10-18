using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackDamager : Damager
{
    public ActiveRangeAttackUser user { get; set; }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            Damage realDamage = _damage;

            DebuffInfo debuffInfo = collision.GetComponent<DebuffInfo>();
            if (debuffInfo)
            {
                if (user.isDebuffDamaging)
                    realDamage.additionalDamage = debuffInfo.abnormalCount * user.additionalDamage;

                if (user.isDebuffTimeUp)
                    debuffInfo.DebuffTimeUp();

                if (user.isRandomDebuff)
                    user.RandomDebuff(debuffInfo);
            }

            // test
            Vector2 dir = CommonFuncs.CalcDir(transform, collision);
            damagable.HitDamager(realDamage, dir);
        }
    }
}
