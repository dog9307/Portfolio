using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritSpawnedEnemyDamager : MonoBehaviour
{
    public Damagable liatris { get; set; }
    public float damage { get; set; }

    private Damagable _damagable;

    void Start()
    {
        _damagable = GetComponent<Damagable>();
    }

    private bool _prevIsDie;
    void Update()
    {
        if (!_prevIsDie && _damagable.isDie)
        {
            HitDamager();
            enabled = false;
        }
        _prevIsDie = _damagable.isDie;
    }

    public void HitDamager()
    {
        if (!liatris) return;

        liatris.currentHP -= damage;
    }
}
