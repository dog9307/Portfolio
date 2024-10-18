using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeSSackController : MonoBehaviour
{
    [SerializeField]
    GameObject _following;

    Animator _animator;
    Rigidbody2D _rigid;

    Vector3 _sight;
    public Vector3 sight { get { return _sight; } }

    [SerializeField]
    bool _isScare;
    public bool IsScare { get { return _isScare; } }
    [SerializeField]
    bool _isMove;
    public bool IsMove { get { return _isMove; } }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigid = GetComponent<Rigidbody2D>();
        _isMove = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (_rigid.velocity.magnitude == 0.0f)
        {
            _isMove = false;
        }
        else _isMove = true;


        if (_following)
        {
            _sight = CommonFuncs.CalcDir(transform.position, _following.transform.position);
        }
        else
        {
            _sight = Vector3.down;
        }

    }
    public void Suprise()
    {
        if (_isScare)
        {
            _animator.SetTrigger("Suprise");
        }
        else return;
    }
    public void Jump()
    {
        _animator.SetTrigger("Jump");        
    }
}
