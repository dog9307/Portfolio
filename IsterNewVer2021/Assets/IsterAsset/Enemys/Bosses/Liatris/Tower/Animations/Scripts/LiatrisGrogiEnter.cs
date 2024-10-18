using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisGrogiEnter : StateMachineBehaviour
{
    [SerializeField]
    private bool _isBossDie = false;

    [SerializeField]
    private bool _isSFXOn = false;
    private SFXPlayer _sfx;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<LiatrisController>().GrogiEnter();

        animator.GetComponentInParent<LiatrisController>()._isCanPattern = false;

        if (_isBossDie)
            animator.GetComponentInParent<LiatrisController>().BossDie();

        if (_isSFXOn)
        {
            if (!_sfx)
                _sfx = animator.GetComponentInParent<LiatrisController>().gameObject.GetComponentInChildren<SFXPlayer>();

            if (_sfx)
                _sfx.PlaySFX("grogi");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<BossAniCotroller>()._isFirstPhase = false;
    }
}
