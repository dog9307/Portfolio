using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTalkFrom : TalkFrom
{
    [SerializeField]
    protected int _itemID;
    public int itemID { get { return _itemID; } }

    protected PlayerInventory _inventory;

    [SerializeField]
    protected bool _isDestroy = true;

    void Start()
    {
        _inventory = FindObjectOfType<PlayerInventory>();
    }
}
