using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalMirage : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ActiveMirageUser activeMirage = FindObjectOfType<ActiveMirageUser>();
        if (!activeMirage) return;

        if (activeMirage.isAdditionalMirage)
        {
            MirageController origin = animator.GetComponent<MirageController>();
            if (!origin.isSecondShoot)
            {
                GameObject mirage = activeMirage.CreateObject();
                mirage.transform.position = origin.transform.position;

                MirageController newController = mirage.GetComponent<MirageController>();
                newController.isSecondShoot = true;
                newController.damageMultiplier = origin.damageMultiplier;
                newController.dir = origin.dir;
            }
        }
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
