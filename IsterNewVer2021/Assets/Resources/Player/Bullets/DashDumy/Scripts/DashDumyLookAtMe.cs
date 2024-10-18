using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDumyLookAtMe : MonoBehaviour
{
    public ActiveDashUser user { get; set; }

    private List<EnemyBase> _affectedEnemies = new List<EnemyBase>();

    [SerializeField]
    private EventTimer _timer;
    
    private Damagable _damagable;

    // Start is called before the first frame update
    void Start()
    {
        _timer.totalTime += user.additionalDumyTime;
        _timer.AddEvent(DumyDie);

        _damagable = GetComponentInParent<Damagable>();
        _damagable.totalHP += user.additionalDumyHP;
        _damagable.currentHP = _damagable.totalHP;

        Vector3 scale = transform.localScale;
        scale.x *= user.scaleFactor;
        scale.y *= user.scaleFactor;
        transform.localScale = scale;
    }

    public void DumyDie()
    {
        Damage damage = DamageCreator.Create(gameObject, _damagable.currentHP, 0.0f, 1.0f, 0.0f);
        _damagable.HitDamager(damage, Vector2.zero);
    }

    private void OnEnable()
    {
        _affectedEnemies.Clear();
    }

    private void OnDisable()
    {
        GameObject player = FindObjectOfType<PlayerMoveController>().gameObject;
        foreach (EnemyBase enemy in _affectedEnemies)
        {
            if (!enemy) continue;

            enemy.Target = player;
        }

        _affectedEnemies.Clear();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (!enemy) return;

        if (enemy.Target != transform.parent.gameObject)
        {
            enemy.Target = transform.parent.gameObject;
            _affectedEnemies.Add(enemy);
        }
    }

    void EnemyRelease(EnemyBase enemy)
    {
        if (!enemy) return;
        if (!_affectedEnemies.Contains(enemy)) return;

        GameObject player = FindObjectOfType<PlayerMoveController>().gameObject;
        enemy.Target = player;

        _affectedEnemies.Remove(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (!enemy) return;

        EnemyRelease(enemy);
    }
}
