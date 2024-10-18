using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DongleYeeAnimController : AnimController
{
    [SerializeField]
    private DongleYeeMovable _move;

    [SerializeField]
    private DongleYeeAttacker _attack;

    [SerializeField]
    private DongleYeeSight _sight;

    [SerializeField]
    private Damagable _damagable;

    [SerializeField]
    private Rigidbody2D _rigid;
    private bool _isMove;
    [SerializeField]
    private NavMeshAgent _agent;

    void Start()
    {
        _move = GetComponentInParent<DongleYeeMovable>();
        _attack = GetComponentInParent<DongleYeeAttacker>();
        _sight = _attack.gameObject.GetComponentInChildren<DongleYeeSight>();
        _damagable = GetComponentInParent<Damagable>();
        _rigid = GetComponentInParent<Rigidbody2D>();
        _agent = GetComponentInParent<NavMeshAgent>();
        _isMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.enabled)
        {
            _anim.SetFloat("dirX", _sight.dir.x);
            _anim.SetFloat("dirY", _sight.dir.y);
            
            if (_agent.speed < 0.0001f)
            {
                _isMove = false;
            }
            else
            {
                _isMove = true;
            }
        }
        else
        {
            _isMove = false;
        }
        //else
        //{
        //    Vector2 dir = _rigid.velocity.normalized;
        //    _anim.SetFloat("dirX", dir.x);
        //    _anim.SetFloat("dirY", dir.y);
        //    if (_rigid.velocity.magnitude > 0.0f)
        //    {
        //        _isMove = true;
        //    }
        //    else
        //    {
        //        _isMove = false;
        //    }
        //}
      
        _anim.SetBool("isMove", _isMove);
        _anim.SetBool("isHurt", _damagable.isHurt);
        _anim.SetBool("isKnockback", _damagable.isKnockback);
        _anim.SetBool("isDie", _damagable.isDie);

        _anim.SetBool("isAttacking", _attack.isAttacking);
    }
}
