using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritAniController : AnimController
{
    MotherSpiritController _motherController;
    MotherSpiritAttackController _attackController;
    MotherSpiritSpawnController _spawnController;
    MeleeConnecting _meleeConnecting;

    Damagable _damagable;
    // Start is called before the first frame update
    void Start()
    {
        _motherController = GetComponent<MotherSpiritController>();
        _spawnController = GetComponent<MotherSpiritSpawnController>();
        _attackController = GetComponent<MotherSpiritAttackController>();
        _meleeConnecting = GetComponentInChildren<MeleeConnecting>();
        _damagable = GetComponent<Damagable>();

        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isGrogi", _motherController._grogi);
        _anim.SetBool("isGrogiTimeOver", _motherController._grogiTimeOver);
        _anim.SetBool("isSleep", _motherController._isSleep);
        _anim.SetBool("isSpawnEnd", _spawnController._isSpawnEnd);
        _anim.SetBool("isSpawnStart", _motherController._spawnStart);
        _anim.SetBool("isPatternStart", _attackController._patternStart);
        _anim.SetBool("isHeal", _attackController._healStart);
        _anim.SetBool("isAttack", _attackController._attackStart);
        _anim.SetBool("isHurt", _damagable.isHurt);
        _anim.SetBool("isDie", _damagable.isDie);
        _anim.SetInteger("hitStack", _motherController._HitStack);
    }

    public override void Die()
    {
        FindObjectOfType<TowerBossDieCutScene>().StartCutScene();
    }

    public void Grogi()
    {
        _anim.SetTrigger("grogi");
    }
}
