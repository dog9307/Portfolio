using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalDoorTalkFrom : TalkFrom
{
    [SerializeField]
    private ConditionalDoorAnimBase _anim;
    [SerializeField]
    private CutSceneController _cutscene;

    [SerializeField]
    private DoorOpenConditionBase _condition;

    [SerializeField]
    private ConditionalDoor _door;

    private bool _isAlreadyOpen = false;

    [SerializeField]
    private bool _isReOpenable = false;

    [SerializeField]
    private FadingGuideUI _fading;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (_isAlreadyOpen) return;

        if (_condition)
        {
            if (!_condition.IsCanOpenDoor())
            {
                StillClose();

                if (_fading)
                {
                    if (_cor != null)
                        StopCoroutine(_cor);

                    _cor = StartCoroutine(ShowFading());
                }
            }
            else
            {
                _isAlreadyOpen = true;
                OpenDoor();
            }
        }
        else
        {
            _isAlreadyOpen = true;
            OpenDoor();
        }
    }

    private Coroutine _cor;
    IEnumerator ShowFading()
    {
        _fading.StartFading(1.0f);
        yield return new WaitForSeconds(1.0f);
        _fading.StartFading(0.0f);
    }

    public void StillClose()
    {
        if (_sfx)
            _sfx.PlaySFX("still_close");

        if (_anim)
            _anim.CloseAnim();
    }

    public void OpenDoor()
    {
        if (_condition)
            _condition.OpenTodo();

        if (_door)
            _door.UseObject();

        if (_cutscene)
            _cutscene.StartCutScene();
        else
        {
            if (_sfx)
                _sfx.PlaySFX("open");

            if (_anim)
                _anim.OpenAnim();
        }
    }

    public void RecloseDoor()
    {
        if (_anim)
            _anim.ReCloseAnim();

        _isAlreadyOpen = !_isReOpenable;
    }

    public void AlreadyUsed()
    {
        if (_anim)
            _anim.OpenAnim();
    }
}
