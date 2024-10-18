using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BangUlYeeDie : StateMachineBehaviour
{
    REnemyMovable _movable;
    Collider2D _enemyCollider;
    Rigidbody2D _rigid;
    //NavMeshAgent _nevi;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyCollider = animator.GetComponentInParent<Collider2D>();
        _rigid = animator.GetComponentInParent<Rigidbody2D>();
        _movable = animator.GetComponentInParent<REnemyMovable>();
        //_nevi = animator.GetComponentInParent<NavMeshAgent>();

        if (_movable)
            Destroy(_movable);

        _rigid.bodyType = RigidbodyType2D.Static;
        //_nevi.enabled = false;
        _enemyCollider.isTrigger = true;
        _enemyCollider.enabled = false;
    }
    

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
