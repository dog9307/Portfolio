using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueDisposableObject : DisposableObject
{
    [SerializeField]
    private InteractionEvent _dialogue;

    [SerializeField]
    private string _nextDialogueName = "";

    public UnityEvent onUseObject;

    public override void AlreadyUsed()
    {
        base.AlreadyUsed();

        if (_dialogue)
            _dialogue.dialogueName = _nextDialogueName;
    }

    public override void UseObject()
    {
        base.UseObject();

        if (_dialogue)
            _dialogue.dialogueName = _nextDialogueName;

        if (onUseObject != null)
            onUseObject.Invoke();
    }
}
