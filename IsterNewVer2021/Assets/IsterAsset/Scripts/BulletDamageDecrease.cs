using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageDecrease : StateMachineBehaviour
{
    private Damager _damager;
    private float _startDamageMultiplier;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _damager = animator.GetComponent<Damager>();
        if (!_damager) return;

        _startDamageMultiplier = _damager.damage.damageMultiplier;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_damager) return;

        Damage newDamage = _damager.damage;
        newDamage.damageMultiplier = Mathf.Lerp(_startDamageMultiplier, 0.0f, stateInfo.normalizedTime);
        _damager.damage = newDamage;
    }

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
