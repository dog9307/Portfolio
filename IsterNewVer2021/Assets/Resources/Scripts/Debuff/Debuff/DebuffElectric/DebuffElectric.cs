using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffElectric : DebuffBase
{
    private Damagable _ownerDamagable;

    private const float TICK_TIME = 1.0f;
    private float _tickTime;

    public override void Init()
    {
        MAX_TOTAL_TIME = 4.0f;

        base.Init();

        _ownerDamagable = owner.GetComponent<Damagable>();
    }

    public override void Update()
    {
        base.Update();

        _tickTime += IsterTimeManager.enemyDeltaTime;
        if (_tickTime >= TICK_TIME)
        {
            if (_ownerDamagable)
            {
                CreateObject();

               // Vector2 dir = owner.GetComponent<EnemyMoveController>().dir;

                Damage damage = DamageCreator.Create(null, 0.0f, 0.0f, 1.0f, 1.0f);
               //_ownerDamagable.HitDamager(damage, -dir);
            }

            _tickTime = 0.0f;
        }
    }

    public override void BuffOn()
    {
        _currentTime = 0.0f;
        _tickTime = TICK_TIME;
    }

    public override void BuffOff()
    {
    }

    public override GameObject CreateObject()
    {
        GameObject effect = GameObject.Instantiate(effectPrefab);
        effect.transform.parent = owner.transform;
        effect.transform.localPosition = Vector2.zero;

        return effect;
    }
}
