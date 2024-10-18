using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillUserHandChanger<T> where T : ActiveBase
{
    protected BuffInfo _buff;
    protected PlayerAttacker _attack;

    protected T _skill;
    protected HAND _relativeHand;
    public HAND relativeHand {  get { return _relativeHand; } }
    protected HAND _prevHand;

    public abstract void BuffOn();
    public abstract void BuffOff();

    public void Init(BuffInfo buff, PlayerAttacker attack, T skill, HAND hand)
    {
        _buff = buff;
        _attack = attack;

        _skill = skill;
        _relativeHand = hand;

        if (_attack.currentHand != _relativeHand)
            BuffOff();
    }

    public virtual void Update()
    {
        if (_prevHand != _attack.currentHand)
        {
            if (IsCorrectHand())
                BuffOn();
            else
                BuffOff();
        }

        _prevHand = _attack.currentHand;
    }

    public bool IsCorrectHand()
    {
        return (_attack.currentHand == _relativeHand);
    }
}
