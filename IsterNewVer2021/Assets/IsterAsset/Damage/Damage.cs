using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Damage
{
    [Tooltip("총 데미지 : (damage + additionalDamage) * damageMultiplier")]
    public float damage;
    public float damageMultiplier;
    public float additionalDamage;

    public float realDamage { get { return (damage + additionalDamage) * damageMultiplier; } }

    [Tooltip("넉백 수치")]
    public float knockbackFigure;

    [Tooltip("표식 터트리는지??")]
    public bool isMarkerDestroy;

    [Tooltip("관련 오브젝트")]
    public GameObject owner;
}

public class DamageCreator
{
    public static Damage Create(GameObject creator, float damage, float additional, float multi, float knockback = 25.0f)
    {
        Damage temp = new Damage();
        temp.owner = creator;
        temp.damage = damage;
        temp.additionalDamage = additional;
        temp.damageMultiplier = multi;

        temp.knockbackFigure = knockback;

        return temp;
    }
}