using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NellTalkFrom : NpcTalkFrom
{
    [SerializeField]
    private InteractionEvent _dialogue;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        //base.Talk(currentPlayer);
        base.Talk();

        if (_dialogue)
            _dialogue.StartDialogue();
    }
}
