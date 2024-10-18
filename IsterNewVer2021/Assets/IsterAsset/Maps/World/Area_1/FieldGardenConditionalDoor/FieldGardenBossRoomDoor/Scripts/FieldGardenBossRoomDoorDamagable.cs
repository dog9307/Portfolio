using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGardenBossRoomDoorDamagable : Damagable
{
    [SerializeField]
    private TowerGardenDoorAnim _doorAnim;

    public override void HitDamager(Damage damage, Vector2 dir)
    {
        damage.damage = 1.0f;
        damage.additionalDamage = 0.0f;
        damage.damageMultiplier = 1.0f;
        damage.knockbackFigure = 10.0f;

        base.HitDamager(damage, dir);

        if (_doorAnim)
            _doorAnim.CloseAnim();
    }
}
