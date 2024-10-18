using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangUlYeeSpawn : TempBangUlYeePattern
{
    float _patternStartTime;
    float _currentDelay;

    [SerializeField]
    float _patternOffDelay;
    [SerializeField]
    ConditionalDoorTalkFrom _door;

    Coroutine _corutine;

    //스포너
    public GameObject _spawner;
    protected List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

    //소환될 적 , 수
    [SerializeField]
    protected GameObject _enemyPrefab;
    int _spawnCount;
    int _overlapCount;
    int _overlapCount1;

    int enemyNum;

    //소환될 위치
    [SerializeField]
    private Transform[] _spawnPoses;

    bool _isSpawnEnd;

    bool _patternStart;

    private List<Damagable> _enemys = new List<Damagable>();

    private void Start()
    {
        _patternStartTime = 2.0f;
        _isSpawnEnd = false;
        _patternStart = false;
    }
    private void Update()
    {
        for (int i = 0; i < _enemys.Count;)
        {
            if (!_enemys[i])
                _enemys.RemoveAt(i);
            else ++i;
        }
        if (_enemys.Count >= 6) return;

        if (!_door.GetComponent<Collider2D>().isActiveAndEnabled && GetComponent<BangUlYeeAttacker>().IsInRange())
        {
            if (!_isSpawnEnd && !_patternStart)
            {
                _currentDelay += IsterTimeManager.deltaTime;
                
                if (_currentDelay > _patternStartTime)
                {
                    PatternOn();
                }
            }
        }
    }
    public void PatternOn()
    {
        _patternStart = true;
        _spawnCount = 2;
        _currentDelay = 0;
        _corutine = StartCoroutine(SpawnEnemys());
    }
    public void PatternOff()
    {
        _patternStart = false;
        _isSpawnEnd = false;
        StopCoroutine(_corutine);
    }
    public override void PatternEnd()
    {
        for (int i = 0; i < _enemys.Count; ++i)
        {
            if (!_enemys[i]) continue;

            Damage damage = DamageCreator.Create(gameObject, _enemys[i].totalHP, 0.0f, 1.0f);
            Vector2 dir = CommonFuncs.CalcDir(this, _enemys[i]);
            _enemys[i].HitDamager(damage, dir);
        }
    }
    IEnumerator SpawnEnemys()
    {
        Spawn(); 

        if (_isSpawnEnd)
        {
            while (_currentDelay < _patternOffDelay)
            {
                _currentDelay += IsterTimeManager.enemyDeltaTime;
                yield return null;
            }
        }

        PatternOff();
    }
    void Spawn()
    {
        if (!_isSpawnEnd)
        {
            _enemySpawners.Clear();
            for (int i = 0; i < _spawnCount;)
            {
                if (_enemys.Count >= 6) break;

                _enemySpawners.Add(Instantiate(_spawner).GetComponent<EnemySpawner>());


                GameObject prefab = _enemyPrefab;
                GameObject enemy = GameObject.Instantiate(prefab);
                
                _enemySpawners[i].enemy = enemy;
                _enemySpawners[i].gameObject.SetActive(true);
                _enemySpawners[i].AddEvent(SpawnEnd);

                enemy.SetActive(false);

                Vector2 pos = _spawnPoses[i].position;

                _enemySpawners[i].transform.position = pos;

                i++;

                Damagable damagable = enemy.GetComponent<Damagable>();
                damagable.totalHP = 11.0f;
                damagable.currentHP = 11.0f;

                _enemys.Add(damagable);
            }
        
            _isSpawnEnd = true;
        }
    }

    public void SpawnEnd(GameObject spawner)
    {
        REnemyBase enemyBase = spawner.GetComponent<EnemySpawner>().enemy.GetComponent<REnemyBase>();
        if (enemyBase)
            enemyBase.Target = FindObjectOfType<PlayerMoveController>().gameObject;

        Destroy(spawner);
    }
}