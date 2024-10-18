using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTalkFromStayKey : TalkFromStayKey
{
    public UnityEvent OnTalk;

    private AudioSource _loop;

    [SerializeField]
    private List<UpdatableComponent> _updatableList;

    public override void UpdateRatio(float deltaTime)
    {
        if (ratio <= 0.0f)
        {
            if (_sfx)
                _loop = _sfx.PlayLoopSFX("charge");
        }

        base.UpdateRatio(deltaTime);

        if (_updatableList != null)
        {
            foreach (var u in _updatableList)
                u.UpdateManually(ratio);
        }
    }

    public override void ResetRatio()
    {
        base.ResetRatio();

        StopLoopSFX();

        if (_updatableList != null)
        {
            foreach (var u in _updatableList)
                u.ResetUpdateManually();
        }
    }

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        enabled = false;

        StopLoopSFX();

        if (OnTalk != null)
            OnTalk.Invoke();
    }

    void StopLoopSFX()
    {
        if (_loop)
        {
            SoundSystem.instance.StopLoopSFX(_loop);
            _loop = null;
        }
    }
}
