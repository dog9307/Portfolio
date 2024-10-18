using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDummyEyeBlinkBehaviours : StateMachineBehaviour
{
    private static int _currentCount = 0;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int rnd = Random.Range(0, 2);

        _currentCount = (_currentCount + 1) % 2;

        animator.SetBool("isEyeBlink", (_currentCount % 2 == 0));
        //animator.SetBool("isEyeBlink", false);
    }
}
