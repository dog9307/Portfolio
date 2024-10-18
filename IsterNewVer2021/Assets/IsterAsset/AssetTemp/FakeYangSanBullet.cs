using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeYangSanBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject _damager;
    private Animator _anim;
    void Start()
    {
        _damager.gameObject.SetActive(false);
        _anim = GetComponent<Animator>();
    }
    void Update()
    {
    }
    void OnDamager()
    {
        _damager.gameObject.SetActive(true);
    }

    void OffDamager()
    {
        _damager.gameObject.SetActive(false);
    }
}