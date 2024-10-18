using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisDieAnimState : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<FieldLiatrisController>().BossDie();
    }
}
