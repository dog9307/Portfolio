using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BangUlYeeMovable : Movable
{
    private BangUlYeeAttacker _attack;

    private static PlayerMoveController _player;

    void Awake()
    {
        _attack = GetComponent<BangUlYeeAttacker>();
    }

    protected override void ComputeVelocity()
    {
        if (_damagable.isDie) return;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        if (!_player)
        {
            return;
        }

        //if (_agent.enabled)
        //{
        //    _agent.speed = speed * IsterTimeManager.enemyTimeScale;
        //    _agent.SetDestination(_player.center);
        //}
    }
}
