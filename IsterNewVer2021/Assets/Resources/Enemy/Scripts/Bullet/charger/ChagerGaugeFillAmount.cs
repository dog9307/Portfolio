using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChagerGaugeFillAmount : MonoBehaviour
{
    private SpriteRenderer _renderer;

    [SerializeField]
    private float _gaugeIncreaseSpeed;
    private float _currentGauge;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.material.SetFloat("_Progress", 0);

        _currentGauge = _renderer.material.GetFloat("_Progress");
    }

    // Update is called once per frame
    void Update()
    {
        _currentGauge += IsterTimeManager.enemyDeltaTime * _gaugeIncreaseSpeed;

       _renderer.material.SetFloat("_Progress",_currentGauge);
    }

    public bool ChargeComplete()
    {
        if (_currentGauge < 1.0f) return false;

        return true;
    }
}
