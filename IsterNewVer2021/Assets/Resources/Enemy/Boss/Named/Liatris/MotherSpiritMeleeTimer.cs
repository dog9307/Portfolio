using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MotherSpiritMeleeTimer : MonoBehaviour
{
    MotherSpirit _mother;

    [SerializeField]
    private MeleeConnecting _zone;

    [SerializeField]
    private float _totalTime = 5.0f;
    [SerializeField]
    private float _currentTime = 0.0f;

    [SerializeField]
    private Image _front;
    [SerializeField]
    private Image _frontDial;

    [HideInInspector]
    public bool _timerOn;

    private MotherSpiritController _con;

    void Start()
    {
        _mother = GetComponentInParent<MotherSpirit>();
        _con = _mother.GetComponent<MotherSpiritController>();
        TimeReset();
    }

    void Update()
    {
        if (_front)
            _front.fillAmount = _currentTime / _totalTime;

        if (_frontDial)
            _frontDial.fillAmount = _currentTime / _totalTime;

        if (!_zone.inMelee) return;
        if (_zone.isAttacking) return;
        if (!_timerOn) return;

        float timeMulti = 1.0f;
        if (_mother._phaseCount == 0)
        {
            timeMulti = 0.8f;
        }
        else if (_mother._phaseCount == 1)
        {
            timeMulti = 1.0f;
        }
        else if (_mother._phaseCount == 2)
        {
            timeMulti = 1.2f;
        }

        if (_con._grogi)
            timeMulti = -2.0f;

        _currentTime += IsterTimeManager.bossDeltaTime * timeMulti;

        if (_currentTime >= _totalTime)
        {
            _currentTime = _totalTime;
            _zone.MeleeAttack();
        }

        if (_currentTime < 0.0f)
            _currentTime = 0.0f;

    }

    public void TimeReset()
    {
        _currentTime = 0.0f;
        _zone.isAttacking = false;
    }
}
