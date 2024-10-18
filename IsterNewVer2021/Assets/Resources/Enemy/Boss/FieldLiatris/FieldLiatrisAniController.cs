using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldLiatrisAniController : MonoBehaviour
{
    [SerializeField]
    Animator _anim;
    [SerializeField]
    BossMain _boss;
    [SerializeField]
    FieldLiatrisController _controller;

    [SerializeField]
    Damagable _damagable;

    [SerializeField]
    protected SFXPlayer _sfx;

    //[SerializeField]
    //private CutSceneController _phaseChange;

    private bool _isFirstPhase;
    private bool _isAlreadyDieCutScene;

    // Start is called before the first frame update
    void Start()
    {
       
        //_isFirstPhase = true;
        _isAlreadyDieCutScene = false;
    }

    // Update is called once per frame
    void Update()
    {
       _anim.SetBool("isActive", _controller._isActive);
       //_anim.SetBool("isHide", _controller._isHide);
        _anim.SetBool("grogiEnd", _controller._grogiTimeEnd);
        //_anim.SetBool("isMeleeAttack", _controller._isMeleeAttack);
        _anim.SetBool("grogi", _controller._grogi);
        //_anim.SetBool("phaseChange", _controller._isPhaseChage);

        _anim.SetBool("isDie", _damagable.isDie);

        _anim.SetFloat("timeMultiplier", IsterTimeManager.bossTimeScale);
    }
    public void Hitted()
    {
        _anim.SetTrigger("Hitted");
    }

    public void Grogi()
    {
        _anim.SetTrigger("Grogi");
        _controller._grogiTimeEnd = false;
        _controller._grogi = true;
    }

    [SerializeField]
    private CutSceneController _bossDieCutScene;
    public void Die()
    {
        if (_isAlreadyDieCutScene) return;

        _isAlreadyDieCutScene = true;

        _anim.SetTrigger("Die");
        _sfx.PlaySFX("liatrisdie");
        _sfx.PlaySFX("tanadie");

        if (_bossDieCutScene)
            _bossDieCutScene.StartCutScene();
    }
}