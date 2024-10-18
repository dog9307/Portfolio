using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTriggerStateUpdate : SFXTriggerStateBase
{
    [SerializeField]
    [Range(0.0f, 1.0f)] private float _triggerringNormalizedTime = 0.3f;
    [SerializeField]
    private bool _isLoop = true;
    private float _prevTime = 0.0f;
    private bool _isTriggered = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        _isTriggered = false;
        _prevTime = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float currentNormalizedTime = stateInfo.normalizedTime - Mathf.Floor(stateInfo.normalizedTime);
        if (_isLoop)
        {
            if (_prevTime > currentNormalizedTime)
                _isTriggered = false;
        }

        if (!_isTriggered)
        {
            if (currentNormalizedTime > _triggerringNormalizedTime)
            {
                base.TriggerSFX(_sfxName);
                _isTriggered = true;
            }
        }

        _prevTime += currentNormalizedTime - _prevTime;
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
