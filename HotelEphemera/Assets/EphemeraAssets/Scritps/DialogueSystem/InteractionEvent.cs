using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionEvent : MonoBehaviour
{
    [SerializeField]
    private string _dialogueName;
    public string dialogueName { get { return _dialogueName; } set { _dialogueName = value; } }

    public UnityEvent OnDialogueEnd;

    [SerializeField]
    private InteractionConditionBase _condition;
    public InteractionConditionBase condition { set { _condition = value; } }

    [SerializeField]
    private AlertSignal _alertSignal;

    //[SerializeField]
    //private List<DialogueNodeChanger> _changers = new List<DialogueNodeChanger>();

    public void StartDialogue()
    {
        //foreach (var c in _changers)
        //{
        //    if (!c) continue;

        //    if (c.ChangeDialogue())
        //        break;
        //}

        if (_condition)
        {
            if (_condition.IsCanInteraction())
            {
                DialogueManager.instance.AddEndEvent(DialogueEnd);
                InteractManager.instance.StartDialogue(gameObject);
            }
            else
                _alertSignal?.Signal();
        }
        else
        {
            DialogueManager.instance.AddEndEvent(DialogueEnd);
            InteractManager.instance.StartDialogue(gameObject);
        }
    }

    public void DialogueEnd()
    {
        if (OnDialogueEnd != null)
            OnDialogueEnd.Invoke();
    }

    //[SerializeField]
    //private DisposableObject _disposable;
    //public void UseDisposable()
    //{
    //    if (!_disposable) return;

    //    _disposable.UseObject();
    //}
}
