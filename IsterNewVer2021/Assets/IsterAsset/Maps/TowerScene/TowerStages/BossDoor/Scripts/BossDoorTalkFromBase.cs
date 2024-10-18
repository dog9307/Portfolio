using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossDoorTalkFrom : TalkFrom
{
    [SerializeField]
    protected BossDoorCloseAnimBase _anim;
    [SerializeField]
    protected BossDoorOpenCutSceneBase _cutscene;

    [SerializeField]
    protected DoorOpenConditionBase _condition;

    private bool _isAlreadyOpen = false;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (_isAlreadyOpen) return;
        if (!_condition) return;

        if (!_condition.IsCanOpenDoor())
        {
            StillClose();
        }
        else
        {
            _isAlreadyOpen = true;
            OpenDoor();
        }
    }

    public void StillClose()
    {
        if (_anim)
            _anim.CloseAnim();
    }

    public void OpenDoor()
    {
        _condition.OpenTodo();

        if (_cutscene)
            _cutscene.StartOpenCutScene();
        else
        {
            if (_anim)
                _anim.OpenAnim();
        }
    }
}
