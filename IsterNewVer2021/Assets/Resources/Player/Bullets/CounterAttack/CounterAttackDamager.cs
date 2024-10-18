using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackDamager : Damager
{
    public Collider2D target { get; set; }
    public Vector2 dir { get; set; }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
            damagable.HitDamager(_damage, dir);
    }

    protected override bool IsIgnore(Collider2D collision)
    {
        return collision != target;
    }
}
