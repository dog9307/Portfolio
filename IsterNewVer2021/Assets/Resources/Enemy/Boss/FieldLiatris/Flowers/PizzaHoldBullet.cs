using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaHoldBullet : Damager
{
    [SerializeField]
    FieldPizzaFlower _pizza;
    EnemyBulletController bullet;

    //[SerializeField]
    //private float _bulletDamage;

    protected override void Start()
    {
        base.Start();
        gameObject.layer = LayerMask.NameToLayer("Bullets");
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        bullet = GetComponent<EnemyBulletController>();

        if (bullet.isCollisionDestroy)
        {
            bullet.DestroyBullet();
        }


        if (_owner.ToString().Equals(collision.tag)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        DebuffInfo debuff = collision.GetComponent<DebuffInfo>();
        if (damagable)
        {
            // _damage = DamageCreator.Create(_damage.owner, _bulletDamage, 0.0f, 1.0f);
            Vector2 dir = CommonFuncs.CalcDir(transform, collision.transform);
            damagable.HitDamager(_damage, dir);

            _pizza._holding = true;

            _pizza.prevSlowDecrease = debuff.slowDecrease;
            debuff.slowDecrease = 1.0f;
        }
    }

    protected override bool IsIgnore(Collider2D collision)
    {
        return (base.IsIgnore(collision) ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"));
    }
}
