using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTreeMovable : FamiliarMoveController
{
    [SerializeField]
    private EventTimer _timer;

    [SerializeField]
    private float _skillUsingTime;
    [SerializeField]
    private float _idleTime;

    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private GameObject _skill;

    protected override void ComputeVelocity()
    {
        _rigid.velocity = Vector2.zero;
    }

    public void SkillStart()
    {
        if (isRelease) return;

        if (_skill)
            _skill.SetActive(true);

        if (_timer)
        {
            _timer.totalTime = _skillUsingTime;
            _timer.AddEvent(SkillEnd);
        }
    }

    public void SkillEnd()
    {
        if (isRelease) return;

        if (_skill)
            _skill.SetActive(false);

        _anim.SetTrigger("skillEnd");

        if (_timer)
        {
            _timer.totalTime = _idleTime;
            _timer.AddEvent(Disappear);
        }
    }

    public void Disappear()
    {
        _anim.SetTrigger("disappear");
    }

    public void Teleport()
    {
        _targetPos.MoveTarget();

        _rigid.position = _targetPos.transform.position;
    }
}
