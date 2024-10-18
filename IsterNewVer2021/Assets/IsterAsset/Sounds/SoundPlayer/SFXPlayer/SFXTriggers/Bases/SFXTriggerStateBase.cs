using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXTriggerStateBase : StateMachineBehaviour, ISFXTrigger
{
    [SerializeField]
    protected string _sfxName;

    private SFXPlayer _sfxPlayer;
    private Animator _anim;

    public virtual void TriggerSFX(string name)
    {
        if (!_sfxPlayer)
        {
            _sfxPlayer = _anim.GetComponent<SFXPlayer>();
            if (!_sfxPlayer)
            {
                _sfxPlayer = _anim.GetComponentInChildren<SFXPlayer>();
                if (!_sfxPlayer)
                    return;
            }
        }

        _sfxPlayer.PlaySFX(name);
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _anim = animator;
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
