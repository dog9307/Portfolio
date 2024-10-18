using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SavableObject : MonoBehaviour
{
    [SerializeField]
    protected string _key = "";
    public string key { get { return _key; } set { _key = value; } }

    [SerializeField]
    protected bool _isMustSave = false;

    public abstract SavableNode[] GetSaveNodes();

    public void AddSaveData()
    {
        if (SavableDataManager.instance)
            SavableDataManager.instance.AddSavableObject(this, _isMustSave);
    }
}
