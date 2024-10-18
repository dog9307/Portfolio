using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageNode : SkillBuffNode
{
    [SerializeField]
    private int _index;
    public int index { get { return _index; } }

    public int buffId { get { return _buffId; } set { _buffId = value; } }
}
