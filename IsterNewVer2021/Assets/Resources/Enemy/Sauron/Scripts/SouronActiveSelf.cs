using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SouronActiveSelf : MonoBehaviour
{
    SauronTimer _sauron;
    [SerializeField]
    float _sauronTimer;

    [SerializeField]
    float _firstShootTime;

    bool firstShoot;
    // Start is called before the first frame update
    void Start()
    {
        firstShoot = true;
        _sauron = GetComponent<SauronTimer>();
    }

    private void Update()
    {
        if (!_sauron.isAttacking && !firstShoot) StartCoroutine(LaserFire());
        else if(!_sauron.isAttacking && firstShoot) StartCoroutine(LaserFireFirst());

        else return;
    }
    IEnumerator LaserFireFirst()
    {
        yield return new WaitForSeconds(_firstShootTime);

        _sauron.StartLaser();
        firstShoot = false;
        StopCoroutine(LaserFireFirst());
    }

    IEnumerator LaserFire()
    {
        yield return new WaitForSeconds(_sauronTimer);

        _sauron.StartLaser();

    }
}
