using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemAniCon : MonoBehaviour
{
    [SerializeField]
    Vine _vine;
    [SerializeField]
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("isAttaking", _vine._isAttacking);
        //_animator.SetBool("isAwake", _vine.isAwake);
        _animator.SetBool("isActive", _vine.isActive);
        _animator.SetBool("isAttackStart", _vine._isAttackStart);
    }

    
}
