using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StackedSkillBuffNode : SkillBuffNode
{
    [SerializeField]
    private Image[] _levels;

    [SerializeField]
    private Color _levelNonActivated;
    [SerializeField]
    private Color _levelActivated;

    public StackedSkillBuffBase relativeBuff
    {
        get
        {
            if (_buff == null)
                return null;

            return (StackedSkillBuffBase)_buff;
        }
    }

    private int _maxLevel;
    public bool isFullActivated
    {
        get
        {
            if (relativeBuff == null)
                return false;

            return relativeBuff.currentLevel == _maxLevel;
        }
    }

    protected virtual void Start()
    {
        _maxLevel = _levels.Length;
        foreach (Image lvl in _levels)
            lvl.color = _levelNonActivated;
    }

    public override bool IsCanActivate()
    {
        return SiblingCheck() && ParentCheck() && !isFullActivated;
    }

    public override bool IsCanNonActivate()
    {
        bool isCan = false;
        if (!ChildrenCheck())
        {
            if (relativeBuff.currentLevel > 1)
                isCan = true;
        }
        else
            isCan = true;

        return isCan && isActivated;
    }

    protected override void ActivateNode()
    {
        SkillUserManager um = FindObjectOfType<PlayerMoveController>().GetComponentInChildren<SkillUserManager>();
        SkillUser user = um.FindUser(relativeSkillName);

        isActivated = true;
        if (_buff == null)
        {
            _buff = SkillBuffNodeFactory.instance.CreateSkillBuff(relativeSkillName, _buffId);
            relativeBuff.Upgrade();
            user.AddBuff(_buff);
        }
        else
        {
            relativeBuff.Upgrade();
            relativeBuff.BuffOn();
        }

        int index = relativeBuff.currentLevel - 1;
        _levels[index].color = _levelActivated;
    }

    protected override void NonActivateNode()
    {
        SkillUserManager um = FindObjectOfType<PlayerMoveController>().GetComponentInChildren<SkillUserManager>();
        SkillUser user = um.FindUser(relativeSkillName);

        if (_buff == null) return;

        relativeBuff.DownGrade();
        relativeBuff.BuffOff();

        int index = relativeBuff.currentLevel;
        _levels[index].color = _levelNonActivated;

        if (relativeBuff.currentLevel <= 0)
        {
            isActivated = false;
            user.RemoveBuff(_buff);
            _buff = null;
        }
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!IsCanActivate()) return;

            ActivateNode();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (!IsCanNonActivate()) return;

            NonActivateNode();
        }

        SetNodeNonActivated(!isActivated);
    
        if (siblingNodeSet)
            siblingNodeSet.SiblingControl(this);

        if (_buff == null) return;

        if (relativeBuff.currentLevel == 0)
        {
            foreach (SkillBuffNode child in children)
            {
                if (child.isSiblingParents)
                {
                    bool isTrue = true;
                    foreach (SkillBuffNode parent in child.parents)
                    {
                        if (parent.isActivated)
                        {
                            isTrue = false;
                            break;
                        }
                    }

                    if (isTrue)
                        child.SetNodeCantActivated(true);
                }
                else
                    child.SetNodeCantActivated(true);
            }
        }
        else
        {
            foreach (SkillBuffNode child in children)
                child.SetNodeCantActivated(!isActivated);
        }
    }
}
