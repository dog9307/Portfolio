using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAttacker : Attacker
{
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private KeyCode _key = KeyCode.F9;

    public override void CreateBullet()
    {
    }

    public override bool IsTriggered()
    {
        bool isTriggered = Input.GetKeyDown(_key);

        if (isTriggered)
        {
            if (_anim)
                _anim.SetTrigger("attack");
        }

        return isTriggered;
    }
}
