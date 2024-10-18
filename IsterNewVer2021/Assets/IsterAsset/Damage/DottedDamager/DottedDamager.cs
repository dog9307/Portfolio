using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedDamager : Damager
{
    [SerializeField]
    private float _delayTime;

    class DottedInfo
    {
        public float currentTime;
        public bool isInCollider;
    }
    private Dictionary<Damagable, DottedInfo> _damagables = new Dictionary<Damagable, DottedInfo>();

    public void AddToDictionary(Damagable damagable)
    {
        if (!_damagables.ContainsKey(damagable))
        {
            DottedInfo info = new DottedInfo();
            damagable.HitDamager(_damage, Vector2.zero);
            _damagables.Add(damagable, info);
        }

        if (!_damagables[damagable].isInCollider)
        {
            _damagables[damagable].isInCollider = true;
            _damagables[damagable].currentTime = 0.0f;
        }
    }

    void Update()
    {
        foreach (var pair in _damagables)
        {
            DottedInfo info = pair.Value;
            if (!info.isInCollider) continue;
            
            info.currentTime += IsterTimeManager.enemyDeltaTime;
            if (info.currentTime >= _delayTime)
            {
                pair.Key.HitDamager(_damage, Vector2.zero);

                info.currentTime = 0.0f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (_owner.ToString().Equals(collision.tag)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
            AddToDictionary(damagable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_owner.ToString().Equals(collision.tag)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            if (!_damagables.ContainsKey(damagable)) return;

            _damagables[damagable].isInCollider = false;
        }
    }
}
