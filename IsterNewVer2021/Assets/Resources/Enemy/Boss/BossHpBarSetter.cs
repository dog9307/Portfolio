using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHpBarSetter : MonoBehaviour
{
    [HideInInspector]
    public BossBase _boss;

    [HideInInspector]
    public BossControllerBase _bossController;

    public GameObject _bossHpBar;
    Damagable _bossDamagable;
    //체간 보스
    private bool _isPosture;

    private float _maxGauge;
    private float _currentGauge;

    private int _currentPhase;
    
    // Start is called before the first frame update
    //private bool 
    void Start()
    {
        _bossController = GetComponent<BossControllerBase>();
        _bossDamagable = _bossController.GetComponent<Damagable>();
        _boss = _bossController.GetComponent<BossBase>();
        _isPosture = _boss._isWaveBoss;

        _maxGauge = _bossDamagable.totalHP;


        if (_isPosture)
        {
            //_bossHpBar = Resources.Load();
        } 
        else
        {
            //_bossHpBar = Resources.Load();
        }
        //_currentBoss.

        
    }

    // Update is called once per frame
    void Update()
    {
        _currentGauge = _bossDamagable.currentHP;
        
        //_boss._isWaveBoss
        // _boss._phaseCount
    }
    public void PostureHpBarSet()
    {

    }
    public void GaugeHpBarSet()
    {
        _maxGauge = _bossDamagable.totalHP;
        _currentGauge = _bossDamagable.currentHP;
    }
}
