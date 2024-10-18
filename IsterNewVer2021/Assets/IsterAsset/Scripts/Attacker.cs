using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    protected bool _isAttacking;
    public bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }

    [SerializeField]
    protected EnemyAttackEffector _attackEffector;

    void Update()
    {
        if (IsTriggered())
            AttackStart();
    }

    public virtual void AttackStart()
    {
        _isAttacking = true;

        if (!_attackEffector)
            _attackEffector = GetComponentInChildren<EnemyAttackEffector>();

        if (_attackEffector)
            _attackEffector.StartAttackEffect();
    }

    public virtual void AttackEnd()
    {
        _isAttacking = false;
    }

    public abstract bool IsTriggered();
    public abstract void CreateBullet();
}
