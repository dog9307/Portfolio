using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherSpiritDamagable : Damagable
{
    [SerializeField]
    private MotherSpiritController _mother;
    private bool _isCanHurt;
    public override bool isCanHurt { get { return _isCanHurt || _mother._grogi; } set { _isCanHurt = value; } }

    void Start()
    {
        isCanHurt = false;
    }

    public override void HitDamager(Damage damage, Vector2 dir)
    {
        damage.additionalDamage = 0.0f;
        damage.damageMultiplier = 1.0f;

        if (currentHP > totalHP / 3.0f) 
            damage.damage = totalHP / 6.0f;
        else
            damage.damage = currentHP;

        isCanHurt = false;

        base.HitDamager(damage, dir);
    }
    
}
