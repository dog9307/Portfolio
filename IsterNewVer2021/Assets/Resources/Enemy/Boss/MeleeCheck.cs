using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCheck : MonoBehaviour
{
    [SerializeField]
    BossController _bossCon;
    [SerializeField]
    private Collider2D _collider;

    // Start is called before the first frame update 
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
                _bossCon.inMelee = true;
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
                _bossCon.inMelee = false;
            }
        }
    }
}
