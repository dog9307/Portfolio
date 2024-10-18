using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : StateMachineBehaviour
{
    [SerializeField]
    private float _delayTime = 0.0f;

    private GameObject _owner;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _owner = animator.gameObject;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_owner) return;

        if (animator)
        {
            EventTimer timer = _owner.AddComponent<EventTimer>();
            timer.totalTime = _delayTime;
            timer.AddEvent(Destroy);

            SpriteRenderer renderer = animator.GetComponent<SpriteRenderer>();
            if (renderer)
                renderer.enabled = false;

            Shadow shadow = animator.GetComponentInChildren<Shadow>();
            if (shadow)
                shadow.gameObject.SetActive(false);

            Collider2D collider = animator.GetComponent<Collider2D>();
            if (collider)
                collider.enabled = false;
        }
    }

    public void Destroy()
    {
        Destroy(_owner);
    }
}
