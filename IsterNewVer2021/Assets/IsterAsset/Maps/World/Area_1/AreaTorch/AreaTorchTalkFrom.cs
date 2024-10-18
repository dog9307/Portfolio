using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTorchTalkFrom : TalkFrom
{
    [HideInInspector]
    [SerializeField]
    private AreaTorchController _controller;

    [HideInInspector]
    [SerializeField]
    private Animator _anim;

    private AreaTorchManager _manager;

    [SerializeField]
    private CutSceneController _camCutScene;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (!_manager)
            _manager = FindObjectOfType<AreaTorchManager>();

        _manager.TorchSignal();

        if (_sfx)
            _sfx.PlaySFX(_sfxName);

        if (_anim)
        {
            _anim.ResetTrigger("active");
            _anim.SetTrigger("active");
        }

        if (_camCutScene)
            _camCutScene.StartCutScene();
    }
}
