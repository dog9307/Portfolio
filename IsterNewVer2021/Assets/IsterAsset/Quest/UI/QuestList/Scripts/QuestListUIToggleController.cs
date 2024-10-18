using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUIToggleController : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    private bool _isToggleOn = false;

    public bool isUltimateToggle { get; set; }

    [SerializeField]
    private SFXPlayer _sfx;

    // Start is called before the first frame update
    void Start()
    {
        _isToggleOn = false;
        isUltimateToggle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (KeyManager.instance.IsOnceKeyDown("quest_toggle"))
            Toggle();

        _anim.SetBool("isToggle", _isToggleOn && isUltimateToggle);
        if (_isToggleOn)
            _anim.ResetTrigger("update");
    }

    public void Toggle()
    {
        if (_sfx)
            _sfx.PlaySFX("toggle");

        _isToggleOn = !_isToggleOn;
    }

    public void QuestListUpdate()
    {
        if (!_isToggleOn)
        {
            _sfx.PlaySFX("toggle");
            _anim.SetTrigger("update");
        }
    }
}
