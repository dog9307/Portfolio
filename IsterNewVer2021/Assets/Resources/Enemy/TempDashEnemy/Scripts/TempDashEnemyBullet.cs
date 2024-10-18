using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDashEnemyBullet : MonoBehaviour
{
    [SerializeField]
    private Animator _anim;

    [SerializeField]
    private GameObject _endEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _anim.SetTrigger("attackEnd");

        GameObject effect = Instantiate(_endEffect);
        effect.transform.position = transform.position;
    }
}
