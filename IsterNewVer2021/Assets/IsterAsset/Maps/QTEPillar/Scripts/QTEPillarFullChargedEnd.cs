using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTEPillarFullChargedEnd : StateMachineBehaviour
{
    [SerializeField]
    private QTEController _qte;

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_qte)
            _qte = animator.transform.GetComponentInParent<QTEController>();

        _qte.FullChargedEnd();
    }
}
