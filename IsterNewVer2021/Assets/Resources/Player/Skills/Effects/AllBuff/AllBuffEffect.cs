using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBuffEffect : MonoBehaviour, IBuff
{
    private BuffInfo _buff;

    private float _totalTime;
    public float totalTime { get { return _totalTime; } set { _totalTime = value; } }
    private float _currentTime;

    public float figure { get; set; }

    void OnEnable()
    {
        if (!_buff)
            _buff = FindObjectOfType<PlayerMoveController>().GetComponent<BuffInfo>();

        BuffOn();
    }

    void OnDisable()
    {
        BuffOff();
    }

    void Update()
    {
        _currentTime += IsterTimeManager.enemyDeltaTime;
        if (_currentTime >= _totalTime)
            gameObject.SetActive(false);
    }

    public void BuffOn()
    {
        _currentTime = 0.0f;
        _buff.effectSpeedUp += (_buff.effectSpeedUp == 0.0f ? figure : 0.0f);
        _buff.additionalDamage += (_buff.additionalDamage == 0.0f ? 10.0f : 0.0f);
    }

    public void BuffOff()
    {
        _buff.effectSpeedUp -= figure;
        if (_buff.effectSpeedUp < 0.0f)
            _buff.effectSpeedUp = 0.0f;

        _buff.additionalDamage -= 10.0f;
        if (_buff.additionalDamage < 0.0f)
            _buff.additionalDamage = 0.0f;
    }
}
