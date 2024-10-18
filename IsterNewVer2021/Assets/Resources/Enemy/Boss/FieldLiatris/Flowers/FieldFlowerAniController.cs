using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldFlowerAniController : AnimController
{
    [SerializeField]
    private LiatrisFlowers _flower;
    [SerializeField]
    private Damagable _damagable;
    // Start is called before the first frame update
    void Start()
    {
         _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("hitted", _damagable.isHurt);
        _anim.SetBool("die", _damagable.isDie);
        _anim.SetBool("isSpawning", _flower._isSpawnOn);
        _anim.SetBool("isSpawned", _flower._isSpawned);
        _anim.SetBool("isActive", _flower._isActive);       
    }
    public override void Die()
    {
        _flower._trailEffect.gameObject.SetActive(true);
        _flower._coroutine = _flower.StartCoroutine(_flower.FlowerDestroy());
    }
}
