using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerGaugeWithTimer : MonoBehaviour
{
    private float _currentTime;

    public float _maxTime;

    void Start()
    {
        _currentTime = 0;
    }
    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
    }

    public bool ChargeComplete()
    {
        if (_currentTime < _maxTime) return false;

        return true;
    }
}
