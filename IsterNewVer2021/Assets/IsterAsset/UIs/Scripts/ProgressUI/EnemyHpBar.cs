using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBar : ProgressBase
{
    private Damagable _enemyHp;

    public float _hpbarPosY;
    // Start is called before the first frame update
    void Start()
    {
        _enemyHp = GetComponentInParent<Damagable>();
    }

    // Update is called once per frame
    public override void UpdateGauge()
    {
        _MaxGuage = _enemyHp.totalHP;
        _CurretGuage = _enemyHp.currentHP;
        OffHpbar();
    }
    public void HpBarFollow()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _hpbarPosY, transform.position.z); // we need to correct the position of the bar
        this.GetComponent<Transform>().position = Camera.main.WorldToScreenPoint(pos); // we say that the position of the bar is a conversion of the position of the monster in my UI units.
        _firstImage.fillAmount = _CurretGuage;
    }

    public void OffHpbar()
    {
        if(_CurretGuage <= 0)
        {
            this.enabled = false;
        }
    }
}
