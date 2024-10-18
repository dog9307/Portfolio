using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileSettings : MonoBehaviour
{
    [SerializeField]
    private int _targetFPS = 60;

    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = _targetFPS;
    }
}
