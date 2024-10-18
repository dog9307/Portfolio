using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowDamager : Damager
{
    private MagicArrowController _controller;

    [SerializeField]
    private bool isPanetration = false;

    protected override void Start()
    {
        _controller = GetComponent<MagicArrowController>();
        gameObject.layer = LayerMask.NameToLayer("PlayerBullet");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        if (_controller.enabled)
        {
            if (_controller.isCollisionDestroy)
            {
                if (isPanetration)
                {
                    if (collision.gameObject.layer != LayerMask.NameToLayer("Enemys"))
                        _controller.DestroyBullet();
                }
                else
                    _controller.DestroyBullet();
            }

            if (!_controller.isDamaging) return;
        }

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            //Vector2 dir = (collision.transform.position - transform.position).normalized;
            Vector2 dir = CommonFuncs.CalcDir(transform.position, collision);
            damagable.HitDamager(_damage, dir);
        }
    }

    protected override bool IsIgnore(Collider2D collision)
    {
        return (base.IsIgnore(collision) ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"));
    }
}
