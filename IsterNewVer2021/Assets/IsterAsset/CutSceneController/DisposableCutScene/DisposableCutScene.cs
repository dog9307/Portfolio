using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisposableCutScene : DisposableObject
{
    public UnityEvent OnUseObject;

    [SerializeField]
    private bool _isDestroy = true;

    public bool IsCanSeeCutScene()
    {
        //int count = PlayerPrefs.GetInt(_key, _beforeValue);
        int count = SavableDataManager.instance.FindIntSavableData(_key);
        return (count < _afterValue);
    }

    public override void AlreadyUsed()
    {
        base.AlreadyUsed();

        if (_isDestroy)
            Destroy(gameObject);
    }

    public override void UseObject()
    {
        base.UseObject();

        if (OnUseObject != null)
            OnUseObject.Invoke();

        //PlayerPrefs.SetInt(_key, _afterValue);
    }
}
