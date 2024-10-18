using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DisposableObject : SavableObject
{
    [SerializeField]
    protected int _beforeValue;
    [SerializeField]
    protected int _afterValue = 100;

    protected bool _isAlreadyUsed;
    public bool isAlreadyUsed { get { return _isAlreadyUsed; } }

    [SerializeField]
    private List<DisposableObjectHelper> _helperList;

    void Start()
    {
        int count = PlayerPrefs.GetInt(_key, _beforeValue);

        if (count >= _afterValue)
            _isAlreadyUsed = true;

        if (_isAlreadyUsed)
            AlreadyUsed();
    }

    public virtual void AlreadyUsed()
    {
        if (_helperList != null)
        {
            foreach (var h in _helperList)
            {
                if (!h) continue;

                h.AlreadyUsed();
            }
        }
    }

    public virtual void UseObject()
    {
        _isAlreadyUsed = true;

        AddSaveData();
    }

    public override SavableNode[] GetSaveNodes()
    {
        SavableNode[] saves = new SavableNode[1];
        saves[0] = new SavableNode();

        saves[0].key = _key;

        if (_isAlreadyUsed)
            saves[0].value = _afterValue;
        else
            saves[0].value = _beforeValue;

        return saves;
    }
}
