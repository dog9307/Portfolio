using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieEffectFindHelper : MonoBehaviour
{
    [SerializeField]
    private GameObject _relativeObj;
    public GameObject relativeObj { get { return _relativeObj; } }

    void Start()
    {
        _relativeObj.SetActive(false);
    }
}
