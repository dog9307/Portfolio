using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritAttackReset : StateMachineBehaviour
{
    MotherSpiritController _motherSpirit;
    MotherSpiritAttackController _attackController;

    [SerializeField]
    private bool _isMelee = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _motherSpirit = animator.GetComponent<MotherSpiritController>();
        _attackController = animator.GetComponent<MotherSpiritAttackController>();
        _attackController.StateReset();
        if (_isMelee)
        {
            _attackController.GetComponentInChildren<MotherSpiritMeleeTimer>().TimeReset();
        }
        _attackController.GetComponentInChildren<MotherSpiritMeleeTimer>()._timerOn = true; 
        //if (_isMelee)
        _motherSpirit._patternStartOnce = false;
        _motherSpirit._currentTimer = 0;
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
