using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritSpawnController : MonoBehaviour
{
    MotherSpirit _mother;
    MotherSpiritController _motherSpirit;
    //EnemySpawnController _spawnController;
    
    protected GameObject _prefab;
    public GameObject prefab { get { return _prefab; } set { _prefab = value; } }

    public GameObject _spawner;
    protected List<EnemySpawner> _enemySpawners = new List<EnemySpawner>();
    [HideInInspector]
    public List<GameObject> _spawnEnemys = new List<GameObject>();
    //protected List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField]
    protected GameObject _phase1prefab;
    [SerializeField]
    protected GameObject _phase2prefab;
    [SerializeField]
    protected GameObject[] _phase3prefab;

    protected GameObject _currentPrefab;

    [SerializeField]
    private Transform[] _spawnPoses;
    private int spawnCountEachPoses { get { if (_spawnPoses.Length == 0) return _spawnCount; return _spawnCount / _spawnPoses.Length; } }

    public bool _isSpawnEnd;

    public float _spawnDistance;
    protected int _spawnCount;

    [SerializeField]
    //private STAGE _stage;
  
    // Start is called before the first frame update
    void Start()
    {
        _mother = GetComponent<MotherSpirit>();
        _motherSpirit = GetComponent<MotherSpiritController>();
        _isSpawnEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mother._phaseCount == 0)
        {
            //_spawnCount = 16;
            _spawnCount = 4;
                _currentPrefab = _phase1prefab;
            
        }
        else if(_mother._phaseCount == 1)
        {
            //_spawnCount = 12;
            _spawnCount = 4;
                _currentPrefab = _phase2prefab;
            
        }
        else if (_mother._phaseCount == 2)
        {
            _spawnCount = 4;
            
        }
    }
    public void Spawn()
    {
        _motherSpirit._spawnStart = false;

        if (!_isSpawnEnd)
        {
            Damagable damagable = GetComponent<Damagable>();
            float damage = (damagable.totalHP / 6.0f) / (float)_spawnCount;
            _enemySpawners.Clear();
            for (int i = 0; i < _spawnCount;)
            {
                for (int k = 0; k < _spawnPoses.Length; ++k)
                {
                    for (int j = 0; j < spawnCountEachPoses; ++j)
                    {
                        _enemySpawners.Add(Instantiate(_spawner).GetComponent<EnemySpawner>());
                        GameObject prefab;
                        if (_mother._phaseCount == 2) prefab = _phase3prefab[i];
                         
                        else prefab = _currentPrefab;
                        //GameObject prefab = _enemyList[Random.Range(0, _enemyList.Count)];
                        GameObject enemy = GameObject.Instantiate(prefab);

                        MotherSpiritSpawnedEnemyDamager damager = enemy.AddComponent<MotherSpiritSpawnedEnemyDamager>();
                        damager.liatris = damagable;
                        damager.damage = damage;

                        //enemy.GetComponent<EnemyController>()._owner.isPlayerCheck = true;
                        _enemySpawners[i].enemy = enemy;
                        _enemySpawners[i].gameObject.SetActive(true);
                        _enemySpawners[i].AddEvent(SpawnEnd);

                        enemy.SetActive(false);

                        Vector2 pos = _spawnPoses[k].position;
                        float angle = Random.Range(0.0f, 360.0f) * Mathf.Deg2Rad;
                        float distance = Random.Range(5.0f, _spawnDistance);
                        pos += new Vector2(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance);

                        _enemySpawners[i].transform.position = pos;

                        _spawnEnemys.Add(_enemySpawners[i].enemy);
                        i++;
                    }
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
    public bool AllEnemyDie()
    {
        bool isEnd = _isSpawnEnd;
        if (!_isSpawnEnd) return isEnd;

        foreach (GameObject enemy in _spawnEnemys)
        {
            if (!enemy) continue;

            Damagable damagable = enemy.GetComponent<Damagable>();
            if (!damagable.isDie)
            {
                isEnd = false;
                break;
            }
            else
            {
                if (enemy.activeSelf)
                {
                    isEnd = false;
                    break;
                }
            }
        }

        return isEnd;
        //if (_isSpawnEnd)
        //{
        //    for (int i = 0; i < _spawnEnemys.Count; i++)
        //    {
        //        if (!_spawnEnemys[i].gameObject) _spawnEnemys.Remove(_spawnEnemys[i]);

        //        Damagable damagable = _spawnEnemys[i].GetComponent<Damagable>();

        //        if (!damagable.isDie) continue;
                
        //    }
        //    _spawnEnemys.Clear();

        //    return true;
        //}
        //else return false;

        //bool isEnd = _isSpawnEnd;
        //if (!_isSpawnEnd) return isEnd;

        //foreach (GameObject enemy in _spawnEnemys)
        //{
            //if (!enemy) continue;

            //Damagable damagable = enemy.GetComponent<Damagable>();

            //if (!damagable.isDie)
            //{
                //isEnd = false;
                //break;
            //}
            //else
            //{
                //if (enemy.activeSelf)
                //{
                    //isEnd = false;
                    //break;
                //}
            //}
        //}
        //return isEnd;
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var pos in _spawnPoses)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(pos.transform.position, _spawnDistance);
        }
    }
}
