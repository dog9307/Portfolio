using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAniCotroller : MonoBehaviour
{
    [SerializeField]
    public Animator _anim;
    [SerializeField]
    BossMain _boss;
    [SerializeField]
    BossController _controller;
    [SerializeField]
    BossAttacker _attacker;
    [SerializeField]
    BossMovable _movable;

    [SerializeField]
    Damagable _damagable;

    [SerializeField]
    protected SFXPlayer _sfx;

    [SerializeField]
    private CutSceneController _phaseChange;

    [HideInInspector]
    public bool _isFirstPhase;
    private bool _isAlreadyDieCutScene;

    // Start is called before the first frame update
    void Start()
    {
        _isFirstPhase = true;
        _isAlreadyDieCutScene = false;
    }

    // Update is called once per frame
    void Update()
    {

        _anim.SetBool("isFirstPhase", _isFirstPhase);
        _anim.SetBool("isActive", _controller.isActive);
        _anim.SetBool("isHide", _controller._isHide);
        _anim.SetBool("grogiEnd", _controller._grogiTimeEnd);
        _anim.SetBool("isMeleeAttack", _controller._isMeleeAttack);
        _anim.SetBool("grogi", _controller._grogi);
        _anim.SetBool("phaseChange", _controller._isPhaseChage);

        _anim.SetBool("attackPatternStart", _controller.attackPatternStart);
        _anim.SetBool("attackPatternEnd", _controller.attackPatternEnd);
        _anim.SetBool("attackStart", _attacker._attackStart);
        _anim.SetBool("attackEnd", _attacker._attackEnd);

        _anim.SetBool("movePatternStart", _controller.movePatternStart);
        _anim.SetBool("movePatternEnd", _controller.movePatternEnd);
        _anim.SetBool("moveStart", _movable._moveStart);
        _anim.SetBool("moveEnd", _movable._moveEnd);

        _anim.SetBool("isDie", _damagable.isDie);
        _anim.SetBool("isHurt", _damagable.isHurt);
        _anim.SetBool("isCanHurt", _damagable.isCanHurt);

        _anim.SetFloat("timeMultiplier", IsterTimeManager.bossTimeScale);

    }
    public void PhaseChange()
    {
        _anim.SetTrigger("PhageChange");
        _controller._isPhaseChage = true;

        if (_phaseChange)
            _phaseChange.StartCutScene();
    }
    public void Grogi()
    {
        _anim.SetTrigger("Grogi");
        _controller._grogi = true;
    }

    [SerializeField]
    private CutSceneController _bossDieCutScene;
    public void Die()
    {
        if (_isAlreadyDieCutScene) return;

        _isAlreadyDieCutScene = true;

        _anim.SetTrigger("Die");

        if (_bossDieCutScene)
            _bossDieCutScene.StartCutScene();
    }
}
