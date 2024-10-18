using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletDamager : Damager
{
    EnemyBulletController bullet;

    //[SerializeField]
    //private float _bulletDamage;

    [SerializeField]
    private bool _isLayerChange = true;

    [SerializeField]
    bool _isCameraShake;
    [SerializeField]
    float _cameraShakingFigure;

    private void OnEnable()
    {
        if (_isCameraShake)
        {
            CameraShakeController.instance.CameraShake(_cameraShakingFigure);
        }
    }
    protected override  void Start()
    {
        base.Start();

        if (_isLayerChange)
            gameObject.layer = LayerMask.NameToLayer("Bullets");
    }
   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        bullet = GetComponent<EnemyBulletController>();

        if (bullet)
        {
            if (bullet.isCollisionDestroy)
            {
                bullet.DestroyBullet();
            }
        }

        if (_owner.ToString().Equals(collision.tag)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        
        if (damagable)
        {
           // _damage = DamageCreator.Create(_damage.owner, _bulletDamage, 0.0f, 1.0f);
            Vector2 dir = CommonFuncs.CalcDir(transform, collision.transform);
            damagable.HitDamager(_damage, dir);
            if (bullet)
            {
                if (bullet._isTurnBullet) bullet.DestroyBullet();
            }
        }
    }

    protected override bool IsIgnore(Collider2D collision)
    {
        return (base.IsIgnore(collision) ||
            collision.gameObject.layer == LayerMask.NameToLayer("Ground"));
    }
}
