using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNodeChanger : SavableObject
{
    [SerializeField]
    private InteractionEvent _interaction;

    [SerializeField]
    private string _dialogueName;

    [SerializeField]
    private DialogueNodeChangerCondition _condition;

    public bool ChangeDialogue()
    {
        if (_condition)
        {
            if (!_condition.IsCanChange())
                return false;
        }

        _interaction.dialogueName = _dialogueName;

        PlayerPrefs.SetInt(_key, 100);

        return true;
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[1];
        nodes[0] = new SavableNode();

        nodes[0].key = _key;
        nodes[0].value = 100;

        return nodes;
    }
}
