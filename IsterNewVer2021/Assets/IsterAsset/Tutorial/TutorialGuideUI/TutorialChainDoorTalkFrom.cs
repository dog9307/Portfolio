using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChainDoorTalkFrom : TalkFrom
{
    [SerializeField]
    private CutSceneController _chainDoorRemove;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        if (_chainDoorRemove)
            _chainDoorRemove.StartCutScene();
    }
}
