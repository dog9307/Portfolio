using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenDoorAnim : ConditionalDoorAnimBase
{
    [SerializeField]
    private Collider2D _col;
    [SerializeField]
    private TalkFrom _talkFrom;

    [SerializeField]
    private bool _isTriggerOnWithReclose = true;

    [SerializeField]
    private GameObject[] _destroyWithDie;
    [SerializeField]
    private DisposableDestroyedObject _destroyed;

    public override void CloseAnim()
    {
        if (_sfx)
            _sfx.PlaySFX("close");

        _anim.SetTrigger("shake");
    }

    public override void OpenAnim()
    {
        if (_sfx)
            _sfx.PlaySFX("open");

        _anim.SetBool("isOpen", true);

        if (_col)
            _col.enabled = false;

        if (_talkFrom)
            _talkFrom.enabled = false;
    }

    public override void ReCloseAnim()
    {
        if (_sfx)
            _sfx.PlaySFX("open");

        bool isOpen = _anim.GetBool("isOpen");
        if (isOpen)
            _anim.SetBool("isOpen", false);
        else
            _anim.SetTrigger("reClose");

        if (_col)
            _col.enabled = _isTriggerOnWithReclose;

        if (_talkFrom)
            _talkFrom.enabled = false;
    }

    public override void Die()
    {
        _destroyed.UseObject();
    }
}
