using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TowerBattleEventController : MonoBehaviour
{
    [SerializeField]
    protected TowerBattleEventSequence _sequence;

    protected bool _isBattleStart;
    public bool isBattleAllEnd { get; set; }

    [SerializeField]
    protected bool _isReset = false;

    public UnityEvent OnBattleStart;
    public UnityEvent OnBattleEnd;

    [SerializeField]
    protected bool _isBattleArea = false;
     
    void Start()
    {
        _isBattleStart = false;
        isBattleAllEnd = false;
    }

    private void OnEnable()
    {
        if (_isReset && !isBattleAllEnd)
        {
            _isBattleStart = false;
            _sequence.SequenceReset();
        }
    }

    private void OnDisable()
    {
        BattleEnd(false);
    }

    public void BattleStart()
    {
        if (_isBattleStart) return;

        if (OnBattleStart != null)
            OnBattleStart.Invoke();

        PlayerAttacker player = FindObjectOfType<PlayerAttacker>();
        if (player)
        {
            player.isBattle = _isBattleArea;

            if (player.isBattle)
                KeyManager.instance.Disable("tabUI");
        }

        _isBattleStart = true;
        _sequence.SpawnStart(this);
    }

    public void BattleEnd(bool isRealEnd = true)
    {
        PlayerAttacker player = FindObjectOfType<PlayerAttacker>();
        if (player)
        {
            player.isBattle = false;

            if (player.isBattle)
                KeyManager.instance.Enable("tabUI");
        }

        if (!_isBattleStart) return;

        if (isRealEnd)
        {
            isBattleAllEnd = true;

            if (OnBattleEnd != null)
                OnBattleEnd.Invoke();
        }

        KeyManager.instance.Enable("tabUI");
    }
}
