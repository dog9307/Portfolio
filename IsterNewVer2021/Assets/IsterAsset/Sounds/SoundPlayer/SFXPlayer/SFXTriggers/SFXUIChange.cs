using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SFXUIChange : MonoBehaviour
{
    [SerializeField]
    private SFXPlayer _sfx;
    [SerializeField]
    private string _sfxName;

    private GameObject _prevSelectedGameObject;

    [SerializeField]
    private bool _isSkip = true;

    // Update is called once per frame
    void Update()
    {
        if (!_sfx) return;

        if (_prevSelectedGameObject != EventSystem.current.currentSelectedGameObject)
        {
            if (_isSkip)
                _isSkip = false;
            else
                _sfx.PlaySFX(_sfxName);
        }

        _prevSelectedGameObject = EventSystem.current.currentSelectedGameObject;
    }
}
