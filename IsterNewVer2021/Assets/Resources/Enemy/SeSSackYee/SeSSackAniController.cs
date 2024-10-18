using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeSSackAniController : MonoBehaviour
{
    SeSSackController _SeSSack;
    Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _SeSSack = GetComponent<SeSSackController>();
    }
    private void Update()
    { 
        _animator.SetBool("Move", _SeSSack.IsMove);
        _animator.SetBool("Scare", _SeSSack.IsScare);
        _animator.SetFloat("dirX", _SeSSack.sight.x);
        _animator.SetFloat("dirY", _SeSSack.sight.y);
    }
}
