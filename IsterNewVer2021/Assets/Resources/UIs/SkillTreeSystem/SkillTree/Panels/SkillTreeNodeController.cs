using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeNodeController : MonoBehaviour
{
    [SerializeField]
    private string _relativeSkillName;
    private List<SkillBuffNode> _buffNodes = new List<SkillBuffNode>();

    // Start is called before the first frame update
    void Start()
    {
        _buffNodes.Clear();

        SkillBuffNode[] nodes = GetComponentsInChildren<SkillBuffNode>();
        foreach (SkillBuffNode node in nodes)
        {
            node.relativeSkillName = _relativeSkillName;
            _buffNodes.Add(node);
        }
    }
}
