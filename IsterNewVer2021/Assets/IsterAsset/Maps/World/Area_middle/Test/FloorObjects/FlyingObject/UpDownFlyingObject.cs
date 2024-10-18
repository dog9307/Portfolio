using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownFlyingObject : MonoBehaviour
{
    public float _speed;
    public float _updownTimer;

    float _currentTimer;
    // Start is called before the first frame update
    void Start()
    {
        _currentTimer = 0;
        StartCoroutine(DOWN());
    }

    IEnumerator DOWN()
    {
        while(_currentTimer < _updownTimer)
        {
            _currentTimer += IsterTimeManager.deltaTime;

            transform.Translate(new Vector3(0.0f, -_speed * IsterTimeManager.deltaTime ,0.0f));
            yield return null;
        }
        _currentTimer = 0.0f;
        StartCoroutine(UP());
    }
    IEnumerator UP()
    {
        while (_currentTimer < _updownTimer)
        {
            _currentTimer += IsterTimeManager.deltaTime;

            transform.Translate(new Vector3(0.0f, _speed * IsterTimeManager.deltaTime, 0.0f ));
            yield return null;
        }
        _currentTimer = 0.0f;
        StartCoroutine(DOWN());
    }
}
