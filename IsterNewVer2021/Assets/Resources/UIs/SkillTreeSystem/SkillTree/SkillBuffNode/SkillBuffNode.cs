using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillBuffNode : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image _nonActivatedCover;
    [SerializeField]
    private Image _cantActivatedCover;

    public string relativeSkillName { get; set; }
    [SerializeField]
    protected int _buffId;

    [SerializeField]
    private int _cost = 1;
    public int cost { get { return _cost; } set { _cost = value; }  }

    public virtual bool isActivated { get; set; }

    public SiblingNodeSet siblingNodeSet { get; set; }

    [SerializeField]
    private List<SkillBuffNode> _parents = new List<SkillBuffNode>();
    public List<SkillBuffNode> parents { get { return _parents; } }

    [SerializeField]
    private bool _isSiblingParents = false;
    public bool isSiblingParents { get { return _isSiblingParents; } }

    private List<SkillBuffNode> _children = new List<SkillBuffNode>();
    public List<SkillBuffNode> children { get { return _children; } }

    protected SkillBuffBase _buff;

    void Awake()
    {
        isActivated = false;
        foreach (SkillBuffNode parent in _parents)
            parent.children.Add(this);
    }

    void OnEnable()
    {
        if (!ParentCheck())
            SetNodeCantActivated(true);
    }

    protected virtual void ActivateNode()
    {
        SkillUserManager um = FindObjectOfType<PlayerMoveController>().GetComponentInChildren<SkillUserManager>();
        SkillUser user = um.FindUser(relativeSkillName);
        
        isActivated = true;
        if (_buff == null)
            _buff = SkillBuffNodeFactory.instance.CreateSkillBuff(relativeSkillName, _buffId);
        user.AddBuff(_buff);
    }

    protected virtual void NonActivateNode()
    {
        SkillUserManager um = FindObjectOfType<PlayerMoveController>().GetComponentInChildren<SkillUserManager>();
        SkillUser user = um.FindUser(relativeSkillName);
        
        isActivated = false;
        if (_buff == null) return;

        user.RemoveBuff(_buff);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
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

        foreach (SkillBuffNode child in children)
            child.SetNodeCantActivated(!isActivated);

        if (siblingNodeSet)
            siblingNodeSet.SiblingControl(this);
    }

    public bool SiblingCheck()
    {
        bool isCan = true;

        if (siblingNodeSet)
            isCan = siblingNodeSet.SiblingCheck(this);

        return isCan;
    }

    public bool ParentCheck()
    {
        if (_isSiblingParents)
            return SiblingParentCheck();
        else
            return NotSiblingParentCheck();
    }

    bool SiblingParentCheck()
    {
        foreach (SkillBuffNode parent in _parents)
        {
            if (typeof(ActiveCoolTimeDownNode).IsInstanceOfType(parent))
            {
                if (((ActiveCoolTimeDownNode)parent).isIgnore)
                    continue;
            }

            if (parent.isActivated)
                return true;
        }

        return false;
    }

    bool NotSiblingParentCheck()
    {
        foreach (SkillBuffNode parent in _parents)
        {
            if (!parent.isActivated)
                return false;
        }

        return true;
    }

    public bool ChildrenCheck()
    {
        foreach (SkillBuffNode child in _children)
        {
            if (child.isSiblingParents)
            {
                if (child.isActivated)
                {
                    foreach (SkillBuffNode parent in child.parents)
                    {
                        if (parent == this) continue;
                        if (parent.isActivated)
                            return true;
                    }

                    return false;
                }
            }
            else
            {
                if (child.isActivated)
                    return false;
            }
        }

        return true;
    }

    //public bool ChildrenCheck()
    //{
    //    bool isChildActive = false;
    //    SkillBuffNode c = null;
    //    foreach (SkillBuffNode child in _children)
    //    {
    //        if (child.isActivated)
    //        {
    //            isChildActive = true;
    //            c = child;
    //            break;
    //        }
    //    }

    //    if (isChildActive)
    //    {
    //        if (c._isSiblingParents)
    //        {
    //            foreach (SkillBuffNode parent in c._parents)
    //            {
    //                if (parent == this) continue;

    //                if (parent.isActivated)
    //                    return true;
    //            }

    //            return false;
    //        }
    //        else
    //            return false;
    //    }

    //    return true;
    //}

    public virtual bool IsCanActivate()
    {
        return SiblingCheck() && ParentCheck() && !isActivated;
    }

    public virtual bool IsCanNonActivate()
    {
        return ChildrenCheck() && isActivated;
    }

    public void SetNodeCantActivated(bool isCant)
    {
        if (!isCant)
        {
            if (!IsCanActivate())
                return;
        }

        if (isSiblingParents)
        {
            if (SiblingParentCheck())
            {
                if (!isActivated)
                    _nonActivatedCover.gameObject.SetActive(true);
                else
                    _nonActivatedCover.gameObject.SetActive(false);
            }
        }
        else
            _nonActivatedCover.gameObject.SetActive(!isCant);
        _cantActivatedCover.gameObject.SetActive(isCant);
    }

    public void SetNodeNonActivated(bool isNonActivated)
    {
        _nonActivatedCover.gameObject.SetActive(isNonActivated);
    }
}
