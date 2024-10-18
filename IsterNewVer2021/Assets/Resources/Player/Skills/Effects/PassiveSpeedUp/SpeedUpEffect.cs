using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpEffect : MonoBehaviour, IBuff
{
    private BuffInfo _buff;
    private SpriteRenderer _sprite;
    
    private float _totalTime;
    public float totalTime { get { return _totalTime; } set { _totalTime = value; } }
    private float _currentTime;

    public float figure { get; set; }

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }
    
    void OnEnable()
    {
        if (!_buff)
            _buff = FindObjectOfType<BuffInfo>();
        
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

        float alpha = 150.0f * (1.0f - (_currentTime / _totalTime));
        _sprite.color = new Color(1.0f, 1.0f, 1.0f, alpha / 150.0f);
    }

    public void BuffOn()
    {
        _currentTime = 0.0f;
        _buff.effectSpeedUp += (_buff.effectSpeedUp == 0.0f ? figure : 0.0f);
    }

    public void BuffOff()
    {
        _buff.effectSpeedUp -= figure;
        if (_buff.effectSpeedUp < 0.0f)
            _buff.effectSpeedUp = 0.0f;
    }
}
