using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillAnimationEnd : StateMachineBehaviour
{
    protected System.Type _skillType;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public abstract void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex);

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
    
    public void TriggerUser(SkillUserManager manager)
    {
        manager.UseSkill(_skillType);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerSkillUsage skill = animator.GetComponent<PlayerSkillUsage>();
        if (skill)
        {
            if (skill.isSkillUsing)
                TriggerUser(animator.GetComponentInChildren<SkillUserManager>());

            skill.SkillEnd(_skillType);
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
