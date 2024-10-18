using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiblingNodeSet : MonoBehaviour
{
    [SerializeField]
    private List<SkillBuffNode> _siblings = new List<SkillBuffNode>();

    void Start()
    {
        foreach (SkillBuffNode node in _siblings)
            node.siblingNodeSet = this;
    }

    public void SiblingControl(SkillBuffNode current)
    {
        foreach (SkillBuffNode node in _siblings)
        {
            if (node == current) continue;

            node.SetNodeCantActivated(current.isActivated);
        }
    }

    public bool SiblingCheck(SkillBuffNode current)
    {
        bool check = true;
        foreach (SkillBuffNode node in _siblings)
        {
            if (node == current) continue;

            if (node.isActivated)
            {
                check = false;
                break;
            }
        }

        return check;
    }
}
