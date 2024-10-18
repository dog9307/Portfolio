using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleYeeHittedEndCheck : StateMachineBehaviour
{
    [SerializeField]
    private float _drag = 10.0f;

    private Rigidbody2D _rigid;
    private Damagable _damagable;

    private Vector2 _startVelocity;

    private const float MIN_MAGNITUDE = 1.0f;

    private KnockbackDragDownner _dragDownner;

    private BounceDamager[] _bounces;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rigid = animator.GetComponentInParent<Rigidbody2D>();
        _damagable = animator.GetComponentInParent<Damagable>();
        KnockbackDragDownner[] downners = animator.GetComponentsInChildren<KnockbackDragDownner>();
        if (downners.Length > 0)
            _dragDownner = downners[0];
        for (int i = 1; i < downners.Length; ++i)
            Destroy(downners[i].gameObject);

        if (!_rigid) return;

        _startVelocity = _rigid.velocity;
        _rigid.drag = _drag;

        _bounces = animator.GetComponents<BounceDamager>();

        DebuffInfo debuff = _rigid.GetComponent<DebuffInfo>();
        if (debuff)
            _rigid.drag *= (1.0f - debuff.knockbackDragDecrease);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_rigid) return;

        bool isKnockbackEnd = false;
        if (_rigid.velocity.magnitude <= MIN_MAGNITUDE)
            isKnockbackEnd = true;

        float ratio = _rigid.velocity.magnitude / _startVelocity.magnitude;
        if (ratio < 0.1f)
            isKnockbackEnd = true;

        if (isKnockbackEnd)
        {
            _rigid.drag = 0.0f;
            _damagable.isKnockback = false;

            animator.SetBool("isKnockback", _damagable.isKnockback);
            //animator.Update(Time.deltaTime);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_dragDownner)
            _dragDownner.Destroy();

        for (int i = 0; i < _bounces.Length; ++i)
            Destroy(_bounces[i]);
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
