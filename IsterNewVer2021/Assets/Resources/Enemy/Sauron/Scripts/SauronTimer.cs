using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauronTimer : MonoBehaviour
{
    [SerializeField]
    private SauronLaserController _laser;

    public float _readyTime;
    [SerializeField]
    private float _laserTime;

    [SerializeField]
    private bool _isAttacking;
    public bool isAttacking { get { return _isAttacking; } }

    void Start()
    {
        _isAttacking = false;
    }

    public void StartLaser()
    {
        if (_isAttacking) return;

        StartCoroutine(Laser());
    }

    IEnumerator Laser()
    {
        _isAttacking = true;

        _laser.LaserCharging(_readyTime);

        float currentTime = 0.0f;
        while (currentTime < _readyTime)
        {
            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }

        _laser.LaserShoot();

        currentTime = 0.0f;
        while (currentTime < _laserTime)
        {
            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }

        _laser.LaserOff();

        _isAttacking = false;
    }
}
