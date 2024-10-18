using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageAnimStop : StateMachineBehaviour
{
    private PassiveMirageController _controller;
    private Collider2D _collider;
    private Collider2D[] _overlaps = new Collider2D[16];
    private ContactFilter2D _filter = new ContactFilter2D();

    private float _totalTime = 1.0f;
    private float _currentTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        _currentTime = 0.0f;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (animatorStateInfo.normalizedTime >= 0.5f)
        {
            if (!_collider)
                _collider = animator.GetComponent<Collider2D>();
            if (!_controller)
                _controller = animator.GetComponent<PassiveMirageController>();

            bool isArrive = false;
            _collider.OverlapCollider(_filter, _overlaps);
            foreach (Collider2D overlap in _overlaps)
            {
                if (!overlap) continue;

                PassiveMirageTarget target = overlap.GetComponent<PassiveMirageTarget>();
                if (target)
                {
                    Arrive(animator, out isArrive);
                    break;
                }
            }

            if (!isArrive)
                animator.ForceStateNormalizedTime(0.5f);

            float distance = Vector2.Distance(_controller.prevPosition, animator.transform.position);
            if (distance <= 0.01f)
            {
                _currentTime += IsterTimeManager.deltaTime;
                if (_currentTime >= _totalTime)
                    Arrive(animator, out isArrive);
            }
        }
    }

    void Arrive(Animator animator, out bool isArrive)
    {
        _controller.MultiplierIncrease();
        isArrive = true;
        animator.SetBool("isArrive", true);
    }
}
