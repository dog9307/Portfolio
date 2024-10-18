using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicArsenal;

public class SauronLaserController : MonoBehaviour
{
    [SerializeField]
    private MagicBeamStatic _effect;

    [SerializeField]
    private Transform _range;

    private SauronAttackRange _attack;

    private SauronLaserDamager _damager;

    private bool _attackStart;
    public bool attackStart { get { return _attackStart; } set { _attackStart = value; } }
    
    private bool _attackEnd;
    public bool attackEnd { get { return _attackEnd; } set { _attackEnd = value; } }

    private Coroutine _scaling;

    // Start is called before the first frame update
    void Start()
    {
        _attack = GetComponent<SauronAttackRange>();
        _effect.RemoveBeam();

        _attackStart = false;
        _attackEnd = false;
    }

    void Update()
    {
        if (!_damager)
            _damager = GetComponentInChildren<SauronLaserDamager>();

        if (!_damager) return;

        _damager.ColliderRangeRebuild(_effect.beamLength);
    }

    public void LaserCharging(float chargeTime)
    {
        _attack._attackLine.gameObject.SetActive(true);
        _attackStart = true;
        _effect.SpawnBeam();
        _effect.beamLength = 0.0f;

        _scaling = StartCoroutine(RangeScaling(chargeTime));
    }

    IEnumerator RangeScaling(float readyTime)
    {
        float currentTime = 0.0f;

        Vector3 scale = _range.localScale;
        while (currentTime < readyTime)
        {
            float ratio = currentTime / readyTime;

            float scaleFactor = Mathf.Lerp(0.0f, 1.0f, ratio);
            scale = _range.localScale;
            scale.y = scaleFactor;
            _range.localScale = scale;

            _effect.ApplyScale(ratio);

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        
        scale = _range.localScale;
        scale.y = 1.0f;
        _range.localScale = scale;

        _scaling = null;
    }


    public void LaserShoot()
    {
        _attack._attackLine.gameObject.SetActive(false);
        _effect.beamLength = _range.transform.localScale.x;
    }

    public void LaserOff()
    {
        if (_scaling != null)
        {
            StopCoroutine(_scaling);
            _scaling = null;
        }

        _attackEnd = true;
        _effect.RemoveBeam();
    }
}
