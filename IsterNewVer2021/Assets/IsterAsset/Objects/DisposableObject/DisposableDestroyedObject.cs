using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisposableDestroyedObject : DisposableObject
{
    [SerializeField]
    private GameObject[] _destroyedObjects;

    [SerializeField]
    private GameObject _effectPrefab;

    public UnityEvent OnDestroyed;

    public override void AlreadyUsed()
    {
        base.AlreadyUsed();

        foreach (var o in _destroyedObjects)
            o.SetActive(false);

        if (OnDestroyed != null)
            OnDestroyed.Invoke();
    }

    public override void UseObject()
    {
        base.UseObject();
        foreach (var o in _destroyedObjects)
        {
            o.SetActive(false);

            if (_effectPrefab)
            {
                GameObject newEffect = Instantiate(_effectPrefab);
                newEffect.transform.position = o.transform.position;
            }
        }

        if (OnDestroyed != null)
            OnDestroyed.Invoke();
    }
}
