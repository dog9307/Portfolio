using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDie : StateMachineBehaviour
{
    REnemyMovable _movable;
    Collider2D _enemyCollider;
    Rigidbody2D _rigid;
    NavMeshAgent _nevi;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enemyCollider = animator.GetComponent<Collider2D>();
        _rigid =animator.GetComponent<Rigidbody2D>();
         _movable = animator.GetComponent<REnemyMovable>();
        _nevi = animator.GetComponent<NavMeshAgent>();

        if (_movable)
            Destroy(_movable);

        _rigid.bodyType = RigidbodyType2D.Static;
        _nevi.enabled = false;
        _enemyCollider.isTrigger = true;
        _enemyCollider.enabled = false;

        SFXPlayer sfx = animator.GetComponentInChildren<SFXPlayer>();
        if (sfx)
            sfx.PlaySFX("die");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
