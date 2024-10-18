using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeConnecting : MonoBehaviour
{
    [SerializeField]
    private Collider2D _collider;
    private bool _inMelee;
    public bool inMelee { get { return _inMelee; } }

    [SerializeField]
    private ParticleSystem _effect;
    [SerializeField]
    private Animator _anim;

    public bool isAttacking { get; set; }

    // Start is called before the first frame update 
    void Start()
    {
        isAttacking = false;

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
                _inMelee = true;

                _effect.Play();
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
                _inMelee = false;

                _effect.Stop();
            }
        }
    }

    public void MeleeAttack()
    {
        isAttacking = true;
        _anim.SetTrigger("meleeAttackTrigger");
    }
}
