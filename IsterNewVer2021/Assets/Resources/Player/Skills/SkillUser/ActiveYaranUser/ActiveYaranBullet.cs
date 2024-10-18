using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveYaranBullet : MonoBehaviour, IObjectCreator
{
    public struct YaranAttackedEnemy
    {
        public Damagable damagable;
        public ParticleSystem effect;
    }

    private List<YaranAttackedEnemy> _attackedEnemyList = new List<YaranAttackedEnemy>();
    private PlayerInfo _playerInfo;

    [SerializeField]
    private GameObject _readyEffectPrefab;

    [SerializeField]
    private GameObject _attackEffectPrefab;
    public GameObject effectPrefab { get => _attackEffectPrefab; set => _attackEffectPrefab = value; }

    [SerializeField]
    private SFXPlayer _sfx;

    void Start()
    {
        _playerInfo = FindObjectOfType<PlayerInfo>();
    }

    private void OnEnable()
    {
        _attackedEnemyList.Clear();
    }

    private void OnDisable()
    {
        if (_attackedEnemyList.Count > 0)
        {
            Damage damage = new Damage();
            damage.damage = _playerInfo.attackFigure;
            damage.additionalDamage = 0.0f;
            damage.damageMultiplier = 1.0f;
            damage.knockbackFigure = 10.0f;
            damage.owner = gameObject;

            for (int i = 0; i < _attackedEnemyList.Count - 1; ++i)
                Attack(_attackedEnemyList[i], damage);

            damage.damageMultiplier += 0.1f * (_attackedEnemyList.Count - 1);
            Attack(_attackedEnemyList[_attackedEnemyList.Count - 1], damage);

            _attackedEnemyList.Clear();
        }
    }

    void Attack(YaranAttackedEnemy enemy, Damage damage)
    {
        Vector2 dir = CommonFuncs.CalcDir(this, enemy.damagable);
        enemy.damagable.HitDamager(damage, dir);

        GameObject newEffect = CreateObject();
        float angle = Random.Range(0.0f, 360.0f);
        newEffect.transform.position = enemy.damagable.transform.position;
        newEffect.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        enemy.effect.Stop(true);

        if (_sfx)
            _sfx.PlaySFX("attack");
    }

    public GameObject CreateObject()
    {
        GameObject attackEffect = Instantiate(_attackEffectPrefab);
        return attackEffect;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damagable damagable = collision.GetComponent<Damagable>();
        if (!damagable)
        {
            damagable = collision.GetComponentInChildren<Damagable>();
            if (!damagable) return;
        }

        for (int i = 0; i < _attackedEnemyList.Count; ++i)
        {
            if (_attackedEnemyList[i].damagable == damagable)
                return;
        }

        GameObject readyEffect = Instantiate(_readyEffectPrefab);
        readyEffect.transform.position = damagable.transform.position;

        YaranAttackedEnemy enemy = new YaranAttackedEnemy();
        enemy.damagable = damagable;
        enemy.effect = readyEffect.GetComponent<ParticleSystem>();

        enemy.effect.transform.parent = enemy.damagable.transform;

        _attackedEnemyList.Add(enemy);

        if (_sfx)
            _sfx.PlaySFX("ready");
    }
}
