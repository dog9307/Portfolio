using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public GameObject _moveNpc;

    public Animator _moveAnimator;

    //놀람(우측 포트레이트)
    public void SurprisedRight()
    {   
        _moveAnimator.SetTrigger("surprisedR");
    }
    //놀람(좌측 포트레이트)
    public void SurprisedLefr()
    {
        _moveAnimator.SetTrigger("surprisedL");
    }
    //떨림
    public void Scared()
    {
        _moveAnimator.SetTrigger("scared");
    }
}
