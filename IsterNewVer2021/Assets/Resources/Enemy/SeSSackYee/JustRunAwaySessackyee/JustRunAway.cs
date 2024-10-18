using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JustRunAway : MonoBehaviour
{
    [SerializeField]
    private JustRunAwaySessackyeeController[] _sessacks;

    private bool _isAlreadyStart = false;

    public void RunAway()
    {
        if (_isAlreadyStart) return;
        _isAlreadyStart = true;

        foreach (var s in _sessacks)
            s.Surprise();
    }
}
