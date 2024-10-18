using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauronAnimController : MonoBehaviour
{
    private Animator _anim;
    private SauronLaserController _sauron;
    private SauronTimer _timer;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _sauron = GetComponent<SauronLaserController>();
        _timer = GetComponent<SauronTimer>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("attackStart", _sauron.attackStart);
        _anim.SetBool("attackEnd", _sauron.attackEnd);
        _anim.SetBool("isAttacking", _timer.isAttacking);
    }
}
