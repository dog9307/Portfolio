using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPattern : BossAttackBase
{
    float _currentDelay;
    [SerializeField]
    float _patternOffDelay;
   
    Coroutine _corutine;

    //스포너
    public GameObject _spawner;
    protected List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();

    //소환될 적 , 수
    [SerializeField]
    protected GameObject[] _enemyPrefab;
    int _spawnCount;
    int _overlapCount;
    int _overlapCount1;

    int enemyNum;

    //소환될 위치
    [SerializeField]
    private Transform[] _spawnPoses;

    bool _isSpawnEnd;

    private List<Damagable> _enemys = new List<Damagable>();

    public override void Update()
    {
        base.Update();

        for (int i = 0; i < _enemys.Count;)
        {
            if (_enemys[i])
                ++i;
            else
                _enemys.RemoveAt(i);
        }
    }
    public override void SetPatternId()
    {
        _patternID = 105;
    }
    public override void PatternOn()
    {
        _spawnCount = 4;
        _currentDelay = 0;
        _isSpawnEnd = false;
        _corutine = StartCoroutine(SpawnEnemys());
    }
    public override void PatternOff()
    {
        _owner.attackPatternEnd = true;
        _isSpawnEnd = false;
        StopCoroutine(_corutine);
    }
    IEnumerator SpawnEnemys()
    {
        _attacker._attackStart = false;
        Spawn();
        //_isSpawnEnd = true;
        if (_isSpawnEnd)
        {
            while(_currentDelay < _patternOffDelay)
            {
                _currentDelay += IsterTimeManager.bossDeltaTime;
                yield return null;
            }
        }

        PatternOff();
    }
    void Spawn()
    {
        if (!_isSpawnEnd)
        {
            _sfx.PlaySFX("spawn");

            _enemySpawners.Clear();
            for (int i = 0; i < _spawnCount;)
            {
                for (int k = 0; k < _spawnPoses.Length; ++k)
                {
                    _enemySpawners.Add(Instantiate(_spawner).GetComponent<EnemySpawner>());
                    //GameObject prefab;

                    //중복방지.

                    //enemyNum = Random.Range(0, _enemyPrefab.Length);
                    //if(_overlapCount > 1) {
                    //    enemyNum = 1;
                    //}
                    //if (enemyNum == 0) _overlapCount++;

                    GameObject prefab = _enemyPrefab[i];
                    GameObject enemy = GameObject.Instantiate(prefab);
              
                    //enemy.GetComponent<EnemyController>()._owner.isPlayerCheck = true;
                    _enemySpawners[i].enemy = enemy;
                    _enemySpawners[i].gameObject.SetActive(true);
                    _enemySpawners[i].AddEvent(SpawnEnd);

                    enemy.SetActive(false);

                    Vector2 pos = _spawnPoses[k].position;

                    _enemySpawners[i].transform.position = pos;

                    Damagable dam = enemy.GetComponent<Damagable>();
                    if (dam)
                        _enemys.Add(dam);

                    i++;               
                }
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

    public void BossDie()
    {
        for (int i = 0; i < _enemys.Count; ++i)
        {
            if (!_enemys[i]) continue;

            Damage damage = DamageCreator.Create(_owner.gameObject, _enemys[i].totalHP, 0.0f, 1.0f);
            Vector2 dir = CommonFuncs.CalcDir(_owner, _enemys[i]);
            _enemys[i].HitDamager(damage, dir);
        }
    }
}
