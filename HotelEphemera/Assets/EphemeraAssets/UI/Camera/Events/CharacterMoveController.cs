using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public GameObject _moveNpc;

    public Animator _moveAnimator;

    //���(���� ��Ʈ����Ʈ)
    public void SurprisedRight()
    {   
        _moveAnimator.SetTrigger("surprisedR");
    }
    //���(���� ��Ʈ����Ʈ)
    public void SurprisedLefr()
    {
        _moveAnimator.SetTrigger("surprisedL");
    }
    //����
    public void Scared()
    {
        _moveAnimator.SetTrigger("scared");
    }
}
