using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangUlMeleeCheck : MonoBehaviour
{
    [SerializeField]
    BangUlYeeMelee _bangUl;
    [SerializeField]
    private Collider2D _collider;
    
    void Start()
    {
        if (!_collider)
            _collider = GetComponent<Collider2D>();

        _collider.isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            if (collision.tag.Equals("PLAYER"))
            {
                _bangUl._isMeleeOn = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            if (collision.tag.Equals("PLAYER"))
            {
                _bangUl._isMeleeOn = false;
            }
        }
    }
}
