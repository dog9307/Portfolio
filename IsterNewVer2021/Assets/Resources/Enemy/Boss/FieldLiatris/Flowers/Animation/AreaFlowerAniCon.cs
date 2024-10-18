using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaFlowerAniCon : AnimController
{ 
    [SerializeField]
    private Damagable _damagable;

    FieldLiatrisAreaPattern _liatris;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _liatris = FindObjectOfType<FieldLiatrisAreaPattern>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("hitted", _damagable.isHurt);
        _anim.SetBool("die", _damagable.isDie);
    }
    public override void Die()
    {
        _damagable.isCanHurt = false;
    }
}
