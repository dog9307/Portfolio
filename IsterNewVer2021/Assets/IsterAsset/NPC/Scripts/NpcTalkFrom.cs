using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcTalkFrom : TalkFrom
{
    public UnityEvent onTalk;
    public UnityEvent onTalkEnd;

    [SerializeField]
    private bool _isDialogableNPC = true;

    [SerializeField]
    protected InteractionEvent _interaction;

    //public override void Talk(PlayerInfo currentPlayer)
    public override void Talk()
    {
        LookAtMouse look = FindObjectOfType<LookAtMouse>();
        if (look)
            look.dir = CommonFuncs.CalcDir(look, GetComponent<Collider2D>().bounds.center);

        //currentPlayer.ChangeCharacter(GetComponent<CharacterInfo>());
        if (onTalk != null)
            onTalk.Invoke();

        if (_isDialogableNPC)
        {
            if (_interaction)
            {
                _interaction.StartDialogue();

                DialogueManager.instance.AddEndEvent(OnTalkEnd);
            }
        }
    }

    public virtual void OnTalkEnd()
    {
        if (onTalkEnd != null)
            onTalkEnd.Invoke();

        PlayerMoveController player = FindObjectOfType<PlayerMoveController>();
        if (player)
            player.PlayerMoveFreeze(false);
    }
}
