using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffPoison : DebuffBase
{
    private GameObject _effect;
    private Damagable _ownerDamagable;

    private const float TICK_TIME = 0.5f;
    private float _tickTime;

    public override void Init()
    {
        figure = (1.0f > figure ? 1.0f : figure);
        MAX_TOTAL_TIME = 6.0f;

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
                Damage damage = DamageCreator.Create(null, figure, 0.0f, 1.0f, 0.0f);
                _ownerDamagable.HitDamager(damage, Vector2.zero);
            }

            _tickTime = 0.0f;
        }
    }

    public override void BuffOn()
    {
        if (_effect)
            GameObject.Destroy(_effect);

        _effect = CreateObject();

        _currentTime = 0.0f;
        _tickTime = 0.0f;
    }

    public override void BuffOff()
    {
        if (_effect)
            GameObject.Destroy(_effect);
    }

    public override GameObject CreateObject()
    {
        GameObject effect = GameObject.Instantiate(effectPrefab);
        effect.transform.parent = owner.transform;
        effect.transform.localPosition = Vector2.zero;

        return effect;
    }
}
