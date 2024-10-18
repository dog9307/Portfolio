using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEndBombDamager : Damager
{
    public ActiveGravityUser user { get; set; }

    [SerializeField]
    private float _additionalDamage;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            Damage realDamage = _damage;

            if (user.isDebuffDamaging)
            {
                DebuffInfo debuffInfo = collision.GetComponent<DebuffInfo>();
                if (debuffInfo)
                    realDamage.additionalDamage = debuffInfo.abnormalCount * _additionalDamage;
            }

            // test
            Vector2 dir = CommonFuncs.CalcDir(transform, collision);
            damagable.HitDamager(realDamage, dir);
        }
    }
}
