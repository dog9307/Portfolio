using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusStepOnOff : MonoBehaviour
{
    Animator _animator;
    Collider2D _collider;

    [SerializeField]
    private SFXPlayer _sfx;

    // Start is called before the first frame update
    void Start()
    {//
        _collider = GetComponent<Collider2D>();
        _animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("PLAYER"))
        {
            _sfx.PlaySFX("wave");
            _animator.SetTrigger("StepOnOff");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER"))
        {
            _sfx.PlaySFX("wave");
            _animator.SetTrigger("StepOnOff");
        }
    }
}
