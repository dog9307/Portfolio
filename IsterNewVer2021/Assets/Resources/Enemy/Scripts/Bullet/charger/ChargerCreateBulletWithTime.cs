using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerCreateBulletWithTime : MonoBehaviour
{
    public BulletCreator creator;

    private ChargerGaugeWithTimer _chargeGauge;

    [SerializeField]
    private ParticleSystem _effect;

    void Start()
    {
        _chargeGauge = GetComponentInChildren<ChargerGaugeWithTimer>();
        creator.Reload();
    }
    void Update()
    {
        if (creator.isShoot) return;

        if (_chargeGauge.ChargeComplete())
        {
            creator.FireBullets();
            _effect.Stop();
        }

        return;
    }
}