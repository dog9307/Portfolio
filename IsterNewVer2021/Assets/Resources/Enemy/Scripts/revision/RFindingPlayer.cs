using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class RFindingPlayer : MonoBehaviour
{
    public REnemyBase _owner;
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
        circleCollider.radius = _owner._checkRange;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER") && !_owner._playerCheck)
        {
            _owner.Target = collision.gameObject;
            _owner._playerCheck = true;
        }
    }
}