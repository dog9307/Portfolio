using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTETalkFrom : TalkFrom
{
    [SerializeField]
    private QTEController _controller;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        _controller.Activate();
    }
}
