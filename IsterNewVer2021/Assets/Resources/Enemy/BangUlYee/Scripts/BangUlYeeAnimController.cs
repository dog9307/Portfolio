using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BangUlYeeAnimController : AnimController
{
    [SerializeField]
    private BangUlYeeMovable _move;

    [SerializeField]
    private BangUlYeeAttacker _attack;

    [SerializeField]
    private BangUlYeeSight _sight;

    [SerializeField]
    private Damagable _damagable;

    //[SerializeField]
    //private NavMeshAgent _agent;

    [SerializeField]
    private TempBangUlYeePattern _pattern;

    void Start()
    {
        _move = GetComponentInParent<BangUlYeeMovable>();
        _attack = GetComponentInParent<BangUlYeeAttacker>();
        _damagable = GetComponentInParent<Damagable>();
        //_agent = GetComponentInParent<NavMeshAgent>();
        _sight = GetComponentInParent<BangUlYeeMovable>().gameObject.GetComponentInChildren<BangUlYeeSight>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (_agent.enabled) 
        //{
        //    _anim.SetFloat("dirX", _sight.dir.x);
        //    _anim.SetFloat("dirY", _sight.dir.y);
        //}

        _anim.SetBool("isHurt", _damagable.isHurt);
        _anim.SetBool("isKnockback", _damagable.isKnockback);
        _anim.SetBool("isDie", _damagable.isDie);

        _anim.SetBool("isAttacking", _attack.isAttacking);
    }

    public override void Die()
    {
        base.Die();
        if (_pattern)
            _pattern.PatternEnd();
    }
}
