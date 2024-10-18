using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class FindingPlayer : MonoBehaviour
{
    public EnemyBase _enemyBase;
    private CircleCollider2D circleCollider;

    void OnEnable()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        if (!circleCollider)
            circleCollider = gameObject.AddComponent<CircleCollider2D>();
        circleCollider.isTrigger = true;
    }
    void Start()
    {
        //_enemyBase = GetComponentInParent<EnemyBase>();
    }
    void Update()
    {
        circleCollider.radius = _enemyBase.checkingRange;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("PLAYER") && !_enemyBase.isPlayerCheck)
        {
            _enemyBase.Target = collision.gameObject;
            _enemyBase.isPlayerCheck = true;
        }
    }
}
