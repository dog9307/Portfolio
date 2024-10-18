using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button[] _buttons;

    public void ButtonsFreeze()
    {
        foreach (var b in _buttons)
            b.enabled = false;
    }
}
