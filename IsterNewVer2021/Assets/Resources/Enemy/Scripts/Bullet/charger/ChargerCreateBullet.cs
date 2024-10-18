using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerCreateBullet : MonoBehaviour
{
    private bool _isChargeComplete;
    public bool isChargeComplete { get { return _isChargeComplete; } }

    public BulletCreator creator;

    private ChagerGaugeFillAmount _chargeGauge;

    void Start()
    {
        _isChargeComplete = false;
        _chargeGauge = GetComponent<ChagerGaugeFillAmount>();
        //_chargeGauge = GetComponentInChildren<ChagerGaugeFillAmount>();
        creator.Reload();
    }
  
    void Update()
    {
        if (creator.isShoot) return;

        if (_chargeGauge.ChargeComplete())
        {
            creator.FireBullets();
            Destroy(this.gameObject);
        }

        return;
    }
}