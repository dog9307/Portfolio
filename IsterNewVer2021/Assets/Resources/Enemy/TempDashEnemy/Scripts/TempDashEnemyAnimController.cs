using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempDashEnemyAnimController : AnimController
{
    [SerializeField]
    private TempDashEnemyMovable _move;

    [SerializeField]
    private TempDashEnemyAttacker _attack;

    [SerializeField]
    private TempDashEnemySight _sight;

    [SerializeField]
    private Damagable _damagable;

    [SerializeField]
    private Rigidbody2D _rigid;
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private ParticleSystem[] _effects;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.enabled)
        {
            _anim.SetFloat("dirX", _sight.dir.x);
            _anim.SetFloat("dirY", _sight.dir.y);
        }
        //else
        //{
        //    Vector2 dir = _rigid.velocity.normalized;
        //    _anim.SetFloat("dirX", dir.x);
        //    _anim.SetFloat("dirY", dir.y);
        //

        _anim.SetBool("isHurt", _damagable.isHurt);
        _anim.SetBool("isKnockback", _damagable.isKnockback);
        _anim.SetBool("isDie", _damagable.isDie);

        _anim.SetBool("isAttacking", _attack.isAttacking);
        _anim.SetBool("isReady", _attack._isReady);
    }

    public override void Die()
    {
        base.Die();

        foreach (var e in _effects)
            e.Stop();
    }
}
