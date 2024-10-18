using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisMeleeBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject _damagerArea;

    private float _currentTimer;
    [SerializeField]
    private float _damagerOnTime;
    [SerializeField]
    private float _damagerOffTime;

    public float _bulletTimer;
    // Start is called before the first frame update
    void Start()
    {
        _currentTimer = 0;

        _damagerArea.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _currentTimer += IsterTimeManager.deltaTime;
        
        if(_damagerOnTime <= _currentTimer && _currentTimer < _damagerOffTime)
        {
            _damagerArea.SetActive(true);
        }
        else if( _currentTimer >= _damagerOffTime && _currentTimer < _bulletTimer)
        {
            _damagerArea.SetActive(false);
        }
        else if(_currentTimer > _bulletTimer)
        {
            Destroy(this);
        }
    }
}
