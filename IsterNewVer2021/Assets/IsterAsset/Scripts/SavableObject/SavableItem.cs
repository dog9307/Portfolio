using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavableItem : SavableObject
{
    [SerializeField]
    private int _gainValue = 100;

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] nodes = new SavableNode[1];
        nodes[0] = new SavableNode();

        nodes[0].key = _key;
        nodes[0].value = _gainValue;

        return nodes;
    }
}
