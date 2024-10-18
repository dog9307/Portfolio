using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TowerBattleEventSet
{
    [System.Serializable]
    public struct TowerBattleEventNode
    {
        public Transform pos;
        public GameObject prefab;
    }
    [SerializeField]
    private List<TowerBattleEventNode> _nodes = new List<TowerBattleEventNode>();
    [SerializeField]
    private float _intervalTime = 0.0f;

    private bool _isAllSpawned;
    public bool isSpawned { get; set; }
    private TowerBattleEventSequence _sequence;

    private List<Damagable> _enemyList = new List<Damagable>();

    static private GameObject spawnerPrefab;

    public bool isSpawnAuto { get; set; } = true;
    public bool isSignal { get; set; } = false;

    public FloorList targetList { get; set; }
    public FloorRoofController targetRoof { get; set; }

    public void SpawnAllEnemies(TowerBattleEventSequence se)
    {
        isSpawned = true;
        _isAllSpawned = false;
        _sequence = se;

        if (!spawnerPrefab)
            spawnerPrefab = Resources.Load<GameObject>("EnemySpawnController/EnemySpawner/EnemySpawner");

        _sequence.SpawnCoroutine(SpawnEnemies());
    }

    public IEnumerator SpawnEnemies()
    {
        if (_intervalTime < float.Epsilon)
            _sequence.PlaySpawnSFX();

        foreach (var node in _nodes)
        {
            GameObject spawner = GameObject.Instantiate(spawnerPrefab);
            //spawnerPrefab.transform.position = node.pos.position;
            spawner.transform.parent = node.pos;
            spawner.transform.localPosition = Vector2.zero;
            spawner.transform.parent = null;

            GameObject newEnemy = GameObject.Instantiate(node.prefab);
            newEnemy.SetActive(false);

            ApplyInFloor(newEnemy);

            EnemySpawner es = spawner.GetComponent<EnemySpawner>();
            es.enemy = newEnemy;

            _enemyList.Add(newEnemy.GetComponent<Damagable>());

            if (_intervalTime >= float.Epsilon)
                _sequence.PlaySpawnSFX();

            yield return new WaitForSeconds(_intervalTime);
        }

        _isAllSpawned = true;
    }

    public void ApplyInFloor(GameObject enemy)
    {
        if (targetList)
        {
            List<Collider2D> colList = new List<Collider2D>();
            colList.AddRange(enemy.GetComponentsInChildren<Collider2D>(true));

            List<SortingLayerInfo> infoes = new List<SortingLayerInfo>();
            infoes.AddRange(enemy.GetComponentsInChildren<SortingLayerInfo>(true));

            targetList.AddHelperList(colList, infoes);
        }

        if (targetRoof)
        {
            SpriteRenderer[] sprites = enemy.GetComponentsInChildren<SpriteRenderer>();
            foreach (var s in sprites)
                targetRoof.AddSprite(s);

            Tilemap[] tilemap = enemy.GetComponentsInChildren<Tilemap>();
            foreach (var t in tilemap)
                targetRoof.AddTileMap(t);
        }
    }

    public void Update()
    {
        if (!isSpawned) return;
        if (!_sequence) return;

        if (isSpawnAuto)
        {
            if (IsAllEnemiesDie())
                _sequence.SpawnNextSet();
        }
        else
        {
            if (isSignal)
            {
                if (IsAllEnemiesDie())
                {
                    isSignal = false;

                    _sequence.SpawnNextSet();
                }
            }
        }
    }

    public bool IsAllEnemiesDie()
    {
        if (!isSpawned) return false;
        if (!_isAllSpawned) return false;

        bool isAllDie = true;
        foreach (var enemy in _enemyList)
        {
            if (!enemy.isDie)
            {
                isAllDie = false;
                break;
            }
        }

        return isAllDie;
    }
}
