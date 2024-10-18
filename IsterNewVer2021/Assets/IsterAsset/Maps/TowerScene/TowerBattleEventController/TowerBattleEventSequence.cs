using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TowerBattleEventSequence : MonoBehaviour
{
    [SerializeField]
    private List<TowerBattleEventSet> _sets = new List<TowerBattleEventSet>();
    private int _currentSetIndex;

    private TowerBattleEventController _owner;
    private bool _isSpawnStart;

    [SerializeField]
    private bool _isAutoSpawn = true;

    public UnityEvent OnSpawnEnemies;
    public UnityEvent OnEnemiesSetAllDie;
    private bool _isPrevAllDie;

    [SerializeField]
    private FloorList _targetList;
    [SerializeField]
    private FloorRoofController _targetRoof;

    private Damagable _playerDamagable;
    private bool _isPrevPlayerDie;
    public UnityEvent OnPlayerDie;

    [SerializeField]
    private SFXPlayer _sfx;

    public bool IsCurrentSetEnemiesAllDie()
    {
        if (_currentSetIndex < 0 || _sets.Count <= _currentSetIndex) return false;

        TowerBattleEventSet currentSet = _sets[_currentSetIndex];
        if (currentSet == null) return false;

        return currentSet.IsAllEnemiesDie();
    }

    void Start()
    {
        SequenceReset();
    }

    void Update()
    {
        if (_currentSetIndex < 0 || _sets.Count <= _currentSetIndex) return;

        if (_playerDamagable)
        {
            if (!_isPrevPlayerDie && _playerDamagable.isDie)
            {
                if (OnPlayerDie != null)
                    OnPlayerDie.Invoke();
            }

            _isPrevPlayerDie = _playerDamagable.isDie;

            if (_playerDamagable.isDie)
                return;
        }

        TowerBattleEventSet currentSet = _sets[_currentSetIndex];
        if (currentSet != null)
            currentSet.Update();

        if (!_isPrevAllDie && IsCurrentSetEnemiesAllDie())
        {
            if (OnEnemiesSetAllDie != null)
                OnEnemiesSetAllDie.Invoke();
        }
        _isPrevAllDie = IsCurrentSetEnemiesAllDie();
    }

    public void SpawnStart(TowerBattleEventController con)
    {
        if (!(_currentSetIndex < 0)) return;

        if (!_playerDamagable)
            _playerDamagable = FindObjectOfType<PlayerMoveController>().GetComponent<Damagable>();
        _isPrevPlayerDie = false;

        _owner = con;
        SpawnNextSet();
    }

    public void SpawnNextSet()
    {
        _currentSetIndex++;
        if (_currentSetIndex < _sets.Count)
        {
            TowerBattleEventSet currentSet = _sets[_currentSetIndex];
            if (currentSet != null)
            {
                currentSet.targetList = _targetList;
                currentSet.targetRoof = _targetRoof;

                currentSet.SpawnAllEnemies(this);

                if (OnSpawnEnemies != null)
                    OnSpawnEnemies.Invoke();
            }
        }
        else
            _owner.BattleEnd();
    }

    public Coroutine SpawnCoroutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }

    public void SequenceReset()
    {
        _currentSetIndex = -1;

        foreach (var s in _sets)
            s.isSpawnAuto = _isAutoSpawn;
    }

    public void SpawnSignal()
    {
        if (_currentSetIndex < 0 || _sets.Count <= _currentSetIndex) return;

        TowerBattleEventSet currentSet = _sets[_currentSetIndex];
        if (currentSet != null)
            currentSet.isSignal = true;
    }

    public void PlaySpawnSFX()
    {
        if (_sfx)
            _sfx.PlaySFX("spawn");
    }
}
