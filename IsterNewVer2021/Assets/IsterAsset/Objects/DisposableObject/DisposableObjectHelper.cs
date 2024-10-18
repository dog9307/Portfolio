using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisposableObjectHelper : MonoBehaviour
{
    public UnityEvent OnAlreadyUsed;

    public void AlreadyUsed()
    {
        if (OnAlreadyUsed != null)
            OnAlreadyUsed.Invoke();
    }
}
