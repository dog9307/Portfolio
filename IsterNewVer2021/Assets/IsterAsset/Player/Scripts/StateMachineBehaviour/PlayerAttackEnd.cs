using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackEnd : StateMachineBehaviour
{
    [SerializeField]
    private bool _isCounterAttack = false;
    [SerializeField]
    private float _undamagableTime;

    private Damagable _playerDamagable;

    private AttackChargeEffectController _chargingEffect;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1.0f)
        {
            PlayerDashDelay delay = animator.GetComponent<PlayerDashDelay>();
            delay.DelayStart();

            animator.SetBool("isAttack", false);

            animator.Update(IsterTimeManager.deltaTime);
            
            PassiveChargeAttackUser charge = delay.GetComponent<PlayerSkillUsage>().FindUser<PassiveChargeAttack>() as PassiveChargeAttackUser;
            if (charge)
                charge.EffectOn(false);

            if (_isCounterAttack)
            {
                GameObject player = animator.gameObject;
                _playerDamagable = player.GetComponent<Damagable>();
                _playerDamagable.isCanHurt = false;

                EventTimer timer = player.GetComponent<EventTimer>();
                if (timer)
                    timer.TimerStart();
                else
                {
                    timer = player.AddComponent<EventTimer>();
                    timer.totalTime = _undamagableTime;
                    timer.isEndDestroy = true;
                    timer.AddEvent(UndamagableOff);
                    timer.TimerStart();
                }
            }
        }
    }

    public void UndamagableOff()
    {
        if (!_playerDamagable) return;

        _playerDamagable.isCanHurt = true;
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
