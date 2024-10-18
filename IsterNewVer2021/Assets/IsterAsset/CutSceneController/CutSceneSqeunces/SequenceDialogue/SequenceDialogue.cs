using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceDialogue : CutSceneSqeunceBase
{
    [Header("다이얼로그")]
    [HideInInspector]
    [SerializeField]
    private InteractionEvent _dialogue;

    [SerializeField]
    private bool _isPlayerLookNPC = false;

    protected override IEnumerator DuringSequence()
    {
        if (_isPlayerLookNPC)
            playerLook.dir = CommonFuncs.CalcDir(player, _dialogue);

        _dialogue.StartDialogue();

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();

        while (DialogueManager.instance.isDialogue)
            yield return null;

        _isDuringSequence = false;
    }
}
