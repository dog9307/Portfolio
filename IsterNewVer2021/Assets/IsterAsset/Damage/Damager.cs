using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OWNER
{
    NONE = -1,
    PLAYER,
    ENEMY,
    END
}

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    [SerializeField]
    protected OWNER _owner;
    public OWNER owner { get { return _owner; } set { _owner = value; } }

    public DebuffInfo ownerDebuff { get; set; }
    
    [SerializeField]
    protected Damage _damage;
    public Damage damage { get { return _damage; } set { _damage = value; } }

    [SerializeField]
    private bool _isColliderOff = false;
    [SerializeField]
    private float _offDelayTime = 0.1f;

    protected virtual void Start()
    {
        Damage dmg = damage;
        dmg.owner = gameObject;
        damage = dmg;

        if (_isColliderOff)
            Invoke("ColliderOff", _offDelayTime);
    }

    public void ColliderOff()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsIgnore(collision)) return;

        Damagable damagable = collision.GetComponent<Damagable>();
        if (damagable)
        {
            // test
            Vector2 dir = CommonFuncs.CalcDir(transform, collision);
            damagable.HitDamager(_damage, dir);
        }
    }

    protected virtual bool IsIgnore(Collider2D collision)
    {
        return (_owner.ToString().Equals(collision.tag));
    }
}
