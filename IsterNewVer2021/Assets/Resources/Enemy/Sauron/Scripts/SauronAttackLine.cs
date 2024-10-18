using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauronAttackLine : MonoBehaviour
{
    SauronTimer _sauronTimer;
    SauronAttackRange _attack;

    float _scale;
    float _lowScale;
    // Start is called before the first frame update
    private void OnEnable()
    {
        this.transform.localScale = new Vector3(1.0f,1.0f,1.0f);

        _scale = 1.0f;
        _lowScale = 0.0f;
    }

    private void Start()
    {
        _sauronTimer = GetComponentInParent<SauronTimer>();
        _attack = GetComponentInParent<SauronAttackRange>();
    }
    // Update is called once per frame
    void Update()
    {
        _lowScale = _lowScale + IsterTimeManager.deltaTime;
        _scale = _sauronTimer._readyTime - IsterTimeManager.deltaTime;

            if (_lowScale < 1.0f)
            {
                this.transform.localScale = new Vector3(1.0f, _lowScale);
            }

            if (_scale > 0.0f && _scale < 1.0f)
            {
                this.transform.localScale = new Vector3(1.0f, _scale);
            }
        
        
    }
}
