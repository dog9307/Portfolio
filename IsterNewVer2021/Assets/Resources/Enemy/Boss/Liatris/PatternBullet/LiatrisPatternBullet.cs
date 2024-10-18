using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisPatternBullet : MonoBehaviour
{
    [SerializeField]
    GameObject _floor;
    [SerializeField]
    GameObject _shpere;
    [SerializeField]
    GameObject _damager;
    [SerializeField]
    GameObject _bullet;
    // Start is called before the first frame update
    void Start()
    {
        _damager.SetActive(false);
    }
    public void DamageOn()
    {
        _damager.SetActive(true);
    }
    public void DamageOff()
    {
        _damager.SetActive(false);
    }
    public void BulletOff()
    {
        //GetComponentInParent<LiatrisPlayerTracyingBullet>().ResetBullet();
        _damager.SetActive(false);
        _bullet.SetActive(false);
    }
}
