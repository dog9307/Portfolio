using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NormalLeverTalkFrom : TalkFrom
{
    public UnityEvent OnLeverActive;

    [SerializeField]
    private LeverDisposable _disposable;

    [HideInInspector]
    [SerializeField]
    private Collider2D _col;

    [HideInInspector]
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private float _cameraShakeFigure = 7.0f;

    [SerializeField]
    private GameObject _tuto;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (_disposable)
        {
            if (_disposable.isAlreadyUse) return;

            _disposable.UseObject();
        }
        else
        {
            _col.enabled = false;

            _anim.SetTrigger("on");
        }

        enabled = false;

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        if (_tuto)
            _tuto.SetActive(false);
    }

    public void LeverOn()
    {
        CameraShakeController.instance.CameraShake(_cameraShakeFigure);

        if (_sfx)
            _sfx.PlaySFX("disappear");

        if (OnLeverActive != null)
            OnLeverActive.Invoke();
    }
}
