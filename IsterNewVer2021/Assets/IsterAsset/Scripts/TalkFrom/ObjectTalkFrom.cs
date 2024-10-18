using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectTalkFrom : TalkFrom
{
    public UnityEvent OnTalk;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (OnTalk != null)
            OnTalk.Invoke();
    }
}
