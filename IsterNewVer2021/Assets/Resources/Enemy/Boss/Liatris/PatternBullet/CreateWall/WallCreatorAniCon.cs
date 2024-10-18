using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreatorAniCon : MonoBehaviour
{
    [SerializeField]
    CreateWallPattern _creator;

    [SerializeField]
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetBool("isActive", _creator.isActive);
        //_animator.SetBool("isAwake", _creator.isAwake);
    }
}
