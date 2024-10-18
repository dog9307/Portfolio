using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreatorSpawnPattern : MonoBehaviour
{

    [SerializeField]
    BossController _controller;
    [SerializeField]
    GameObject _wallCreator;

    bool _isSpawn;

    public float _coolTime;
    float _currentCoolTime;

    Coroutine _coroutine;

    [SerializeField]
    List<Transform> _spawnPos = new List<Transform>();

    public float _phase2Multiple;

    [HideInInspector]
    public bool _spawnStart;


    BossDamagableCondition condition;

    // Start is called before the first frame update
    void Start()
    {
        condition = GetComponent<BossDamagableCondition>();
        _currentCoolTime = 0;
        _spawnStart = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        if (condition.isPhaseChanging)
        {
            if (_coroutine != null) { 
                StopCoroutine(_coroutine); 
                _currentCoolTime = 0;  }
        }
        else
        {
            if (_controller.isActive)
            {
                _isSpawn = _wallCreator.gameObject.activeSelf;

                if (!_isSpawn && !_spawnStart)
                {
                    _coroutine = StartCoroutine(CoolTime());
                }
            }
        }
    }
    void SpawnCreator()
    {
        int pos = Random.Range(0, _spawnPos.Count);
        _wallCreator.transform.position = _spawnPos[pos].transform.position;
        _wallCreator.gameObject.SetActive(true);
    }
    IEnumerator CoolTime()
    {
        _spawnStart = true;

        while (_currentCoolTime < _coolTime)
        {
            yield return null;

            if (_controller._isPhaseChage)
                _currentCoolTime += IsterTimeManager.bossDeltaTime * _phase2Multiple;
            else _currentCoolTime += IsterTimeManager.bossDeltaTime;
        }

        SpawnCreator();
        _currentCoolTime = 0;
        StopCoroutine(_coroutine);
        //_currentCoroutine = StartCoroutine(BulletReady());
    }
}
