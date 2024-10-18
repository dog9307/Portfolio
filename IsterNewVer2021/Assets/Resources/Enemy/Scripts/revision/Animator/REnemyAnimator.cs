using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnemyAnimator : MonoBehaviour
{
    [HideInInspector]
    public Animator _animator;
    [HideInInspector]
    public REnemySightController _sight;
    [HideInInspector]
    public REnemyController _enemyController;
    [HideInInspector]
    public Damagable _damagable;
    [HideInInspector]
    public REnemyAttacker _attacker;

    protected void Start()
    {
        _animator = GetComponent<Animator>();
        _damagable = GetComponent<Damagable>();
        _sight = GetComponentInChildren<REnemySightController>();
        _enemyController = GetComponent<REnemyController>();
        _attacker = GetComponent<REnemyAttacker>();
    }

    // Update is called once per frame
    protected void Update()
    {
        
        _animator.SetFloat("dirX", _sight.currentDir.x);
        _animator.SetFloat("dirY", _sight.currentDir.y);
        _animator.SetBool("isInSight", _enemyController._owner._intoSight);
        if (_enemyController.movable)
        {
            _animator.SetBool("moveOn", _enemyController.movable.isActiveAndEnabled);
            _animator.SetBool("isDash", _enemyController.movable._isDash);
            _animator.SetFloat("speed", _enemyController.movable.speed);
        }
        
        _animator.SetBool("attackOn", _enemyController.attacker.isActiveAndEnabled);

        _animator.SetBool("attackStart", _attacker._attackStart);
        _animator.SetBool("isHurt", _damagable.isHurt);
        _animator.SetBool("isKnockback", _damagable.isKnockback);
        _animator.SetBool("isDie", _damagable.isDie);
        _animator.SetBool("isMotionRepeater", _attacker._isRepeat);
    } 
}
