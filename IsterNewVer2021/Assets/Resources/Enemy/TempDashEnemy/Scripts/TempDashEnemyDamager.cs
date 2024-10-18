using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDashEnemyDamager : Damager
{
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private GameObject _endEffect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;
        if (_owner.ToString().Equals(collision.tag)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            Vector2 dir = CommonFuncs.CalcDir(transform, collision.transform);
            damagable.HitDamager(_damage, dir);

            _anim.SetTrigger("attackEnd");

            GameObject effect = Instantiate(_endEffect);
            effect.transform.position = transform.position;
        }
    }
}
